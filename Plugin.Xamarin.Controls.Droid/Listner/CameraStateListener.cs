
using Android.App;
using Android.Hardware.Camera2;
using Plugin.Xamarin.Controls.Droid.Classes;
using Plugin.Xamarin.Controls.Droid.Controls;

namespace Plugin.Xamarin.Controls.Droid.Listner
{
    public class CameraStateListener : CameraDevice.StateCallback
    {
        private readonly CameraDroidView owner;

        public CameraStateListener(CameraDroidView owner)
        {
            if (owner == null)
                throw new System.ArgumentNullException("owner");
            this.owner = owner;
        }

        public override void OnOpened(CameraDevice cameraDevice)
        {
            // This method is called when the camera is opened.  We start camera preview here.
            owner.mCameraOpenCloseLock.Release();
            owner.mCameraDevice = cameraDevice;

            owner.CreateCameraPreviewSession();
        }

        public override void OnDisconnected(CameraDevice cameraDevice)
        {
            owner.mCameraOpenCloseLock.Release();
            cameraDevice.Close();
            owner.mCameraDevice = null;
        }

        public override void OnError(CameraDevice cameraDevice, CameraError error)
        {
            owner.mCameraOpenCloseLock.Release();
            cameraDevice.Close();
            owner.mCameraDevice = null;
            if (owner == null)
                return;
            Activity activity = owner.Activityresult;
            if (activity != null)
            {
                activity.Finish();
            }
        }
    }
}