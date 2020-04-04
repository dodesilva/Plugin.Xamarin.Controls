using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVFoundation;
using CoreGraphics;
using CoreMedia;
using Foundation;
using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.Helpers;
using UIKit;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls.IOS.Classes
{
    public class CameraIOSView : UIView
    {
        public bool IsPreviewing { get; set; }
        AVCaptureVideoPreviewLayer previewLayer;
        AVCaptureDeviceInput captureDeviceInput;
        AVCaptureStillImageOutput stillImageOutput;
        UIPaintCodeButton capturePhotoButton;
        UIPaintCodeButton StopButton;
        UIButton PausePlayButton;
        UIButton flashButton;
        UIButton switchButton;
        UIButton recordButton;
        CameraOptions cameraOptions;

        public event EventHandler<MediaFiles> OnFinichedCaptur;

        public AVCaptureSession CaptureSession { get; private set; }
        public CameraIOSView(CameraOptions options)
        {
            cameraOptions = options;
            IsPreviewing = false;
            SetupUserInterface();
            SetupEventHandlers();
            
        }

        public async Task<bool> AuthorizeCameraUse()
        {
            var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
            if (authorizationStatus != AVAuthorizationStatus.Authorized)
            {
                return await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
            }
            return await Task.FromResult(true);
        }
        private void SetupEventHandlers()
        {
            capturePhotoButton.TouchUpInside += (s, e) =>
            {
                CapturePhoto();
            };
            StopButton.TouchUpInside += (s, e) =>
            {

            };
            PausePlayButton.TouchUpInside += (s, e) =>
            {

            };
            flashButton.TouchUpInside += (s, e) =>
            {
                ToggleFlash();
            };
            switchButton.TouchUpInside += (s, e) =>
            {
                ToggleFrontBackCamera();
            };
            recordButton.TouchUpInside += (s, e) =>
            {

            };
        }


        AVCaptureDevice GetCameraForOrientation(AVCaptureDevicePosition orientation)
        {
            var devices = AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video);

            foreach (var device in devices)
            {
                if (device.Position == orientation)
                {
                    return device;
                }
            }
            return null;
        }
        private async void CapturePhoto()
        {
            try
            {
                if (this.stillImageOutput == null)
                {
                    this.stillImageOutput = new AVCaptureStillImageOutput()
                    {
                        OutputSettings = new NSDictionary()
                    };
                }
                var videoConnection = this.stillImageOutput.ConnectionFromMediaType(AVMediaType.Video);
                var sampleBuffer = await stillImageOutput.CaptureStillImageTaskAsync(videoConnection);
                var imgData = AVCaptureStillImageOutput.JpegStillToNSData(sampleBuffer);
                var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                string jpgFilename = Path.Combine(documentsDirectory, Guid.NewGuid() + ".jpeg");
                NSError err = null;
                if (imgData.Save(jpgFilename, false, out err))
                {
                    var result = new MediaFiles
                    {
                        FileSource = ImageSource.FromStream(imgData.AsStream),
                        FullPath = jpgFilename,
                        ContentByte = imgData.ToArray(),
                        MimeType="image/jpeg",
                        Success=true
                    };
                    OnFinichedCaptur?.Invoke(this, result);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async void SetupUserInterface()
        {
            var centerButtonX = Layer.Bounds.GetMidX() - 35f;
            var bottomButtonY = Layer.Bounds.Bottom - 85;

            var topLeftX = Layer.Bounds.Right - 25;
            var topButtonY = Layer.Bounds.Bottom - 85;

            var topFlLeftX = Layer.Bounds.X+ 25;
            var topFlButtonY = Layer.Bounds.Top + 25;

            var topRecordLeftX = Layer.Bounds.X + 25;
            var topRecordButtonY = Layer.Bounds.Bottom - 85;

            var buttonWidth = 70;
            var buttonHeight = 70;

            capturePhotoButton = new UIPaintCodeButton(DrawButtons.TakePhotoButton);
            capturePhotoButton.Frame = new CGRect(centerButtonX, bottomButtonY, buttonWidth, buttonHeight);

            StopButton = new UIPaintCodeButton(DrawButtons.StopButton);
            StopButton.Frame = new CGRect(centerButtonX, bottomButtonY, buttonWidth, buttonHeight);
            StopButton.Hidden = true;

            PausePlayButton = new UIButton();
            PausePlayButton.Frame = new CGRect(topLeftX, topButtonY, 40, 40);
            PausePlayButton.Hidden = true;
            PausePlayButton.SetBackgroundImage(UIImage.FromFile("Images/pausecircleoutline.png"), UIControlState.Normal);

            switchButton = new UIButton();
            switchButton.Frame = new CGRect(topLeftX, topButtonY, 40,40);
            switchButton.SetBackgroundImage(UIImage.FromFile("Images/switchcamera.png"), UIControlState.Normal);

            flashButton = new UIButton();
            flashButton.Frame = new CGRect(topFlLeftX, topFlButtonY, 40, 40);
            flashButton.SetBackgroundImage(UIImage.FromFile("Images/flashoff.png"), UIControlState.Normal);

            recordButton = new UIButton();
            recordButton.Frame = new CGRect(topRecordLeftX, topRecordButtonY, 60, 60);
            recordButton.SetBackgroundImage(UIImage.FromFile("Images/record.png"), UIControlState.Normal);

            if (await AuthorizeCameraUse())
            {
                SetupLiveCameraStream();
                AddSubview(capturePhotoButton);
                //AddSubview(recordButton);
                //AddSubview(PausePlayButton);
                AddSubview(switchButton);
                AddSubview(switchButton);
                // AddSubview(StopButton);
            }
        }

        private void SetupLiveCameraStream()
        {
            try
            {
                if (CaptureSession == null)
                    previewLayer = new AVCaptureVideoPreviewLayer(CaptureSession)
                    {
                        VideoGravity = AVLayerVideoGravity.ResizeAspectFill,

                        Frame = Bounds
                    };
                // create a device input and attach it to the session
                var videoDevices = AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video);
                var cameraPosition = (cameraOptions == CameraOptions.Front) ? AVCaptureDevicePosition.Front : AVCaptureDevicePosition.Back;
                var device = videoDevices.FirstOrDefault(d => d.Position == cameraPosition);
                captureDeviceInput = AVCaptureDeviceInput.FromDevice(device);

                var dictionary = new NSMutableDictionary();
                dictionary[AVVideo.CodecKey] = new NSNumber((int)AVVideoCodec.JPEG);
                stillImageOutput = new AVCaptureStillImageOutput()
                {
                    OutputSettings = new NSDictionary()
                };

                CaptureSession.AddOutput(stillImageOutput);
                CaptureSession.AddInput(captureDeviceInput);
                Layer.AddSublayer(previewLayer);
                CaptureSession.StartRunning();
            }
            catch (Exception ex)
            {

            }
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            previewLayer.Frame = rect;
        }

        public void ToggleFlash()
        {
            var device = captureDeviceInput.Device;

            var error = new NSError();
            if (device.HasFlash)
            {
                if (device.FlashMode == AVCaptureFlashMode.On)
                {
                    device.LockForConfiguration(out error);
                    device.FlashMode = AVCaptureFlashMode.Off;
                    device.UnlockForConfiguration();
                    flashButton.SetBackgroundImage(UIImage.FromFile("Images/flashoff.png"), UIControlState.Normal);
                }else if (device.FlashMode == AVCaptureFlashMode.Off)
                {
                    device.LockForConfiguration(out error);
                    device.FlashMode = AVCaptureFlashMode.Auto;
                    device.UnlockForConfiguration();
                    flashButton.SetBackgroundImage(UIImage.FromFile("Images/autoflash.png"), UIControlState.Normal);
                }
                else if (device.FlashMode == AVCaptureFlashMode.Auto)
                {
                    device.LockForConfiguration(out error);
                    device.FlashMode = AVCaptureFlashMode.On;
                    device.UnlockForConfiguration();
                    flashButton.SetBackgroundImage(UIImage.FromFile("Images/flashOn.png"), UIControlState.Normal);
                }
            }
        }

        public void ToggleFrontBackCamera()
        {
            var devicePosition = captureDeviceInput.Device.Position;
            if (devicePosition == AVCaptureDevicePosition.Front)
            {
                devicePosition = AVCaptureDevicePosition.Back;
            }
            else
            {
                devicePosition = AVCaptureDevicePosition.Front;
            }

            var device = GetCameraForOrientation(devicePosition);
            ConfigureCameraForDevice(device);

            CaptureSession.BeginConfiguration();
            CaptureSession.RemoveInput(captureDeviceInput);
            captureDeviceInput = AVCaptureDeviceInput.FromDevice(device);
            CaptureSession.AddInput(captureDeviceInput);
            CaptureSession.CommitConfiguration();
        }

        private void ConfigureCameraForDevice(AVCaptureDevice device)
        {
            var error = new NSError();
            if (device.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
            {
                device.LockForConfiguration(out error);
                device.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
                device.UnlockForConfiguration();
            }
            else if (device.IsExposureModeSupported(AVCaptureExposureMode.ContinuousAutoExposure))
            {
                device.LockForConfiguration(out error);
                device.ExposureMode = AVCaptureExposureMode.ContinuousAutoExposure;
                device.UnlockForConfiguration();
            }
            else if (device.IsWhiteBalanceModeSupported(AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance))
            {
                device.LockForConfiguration(out error);
                device.WhiteBalanceMode = AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance;
                device.UnlockForConfiguration();
            }
        }

    }
}