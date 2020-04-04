using Plugin.Xamarin.Controls.IOS.Controls;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Plugin.Xamarin.Controls.EnumFiles;
using System.Threading.Tasks;
using Foundation;
using AVFoundation;
using CoreMedia;
using System.Linq;
using CoreFoundation;
using System.Collections.Generic;
using System.Text;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Interfaces;
using Plugin.Xamarin.Controls.EventArgsFile;
using Plugin.Xamarin.Controls.IOS.Classes;

[assembly: ExportRenderer(typeof(PXC_VideoPlayer), typeof(PXC_VideoPlayerRenderer))]
namespace Plugin.Xamarin.Controls.IOS.Controls
{
    public class PXC_VideoPlayerRenderer : ViewRenderer<PXC_VideoPlayer, VideoSurface>, IVideoPlayback
    {
        public static readonly NSString StatusObservationContext =
            new NSString("AVCustomEditPlayerViewControllerStatusObservationContext");

        public static NSString RateObservationContext =
            new NSString("AVCustomEditPlayerViewControllerRateObservationContext");
        private VideoSurface RenderSurface;
        private AVPlayer _player;
        private VideoStatus _status;
        private AVPlayerLayer _videoLayer;

        public event VideoAudioStatusChangedEventHandler VideoAudioStatusChanged;
        public event VideoAudioProgressChangedEventHandler VideoAudioProgressChanged;
        public event VideoAudioFinishedEventHandler VideoAudioFinishedChanged;
        public event VideoAudioFailedEventHandler VideoAudioFailedChanged;
        public event VideoAudioVolumeEventHandler VideoAudioVolumeChanged;
        public event EventHandler<bool> FullScreenStatusChanged;

        public PXC_VideoPlayerRenderer()
        {

        }

        private AVPlayer Player
        {
            get
            {
                if (_player == null)
                    InitializePlayer();
                return _player;
            }
        }
        private NSUrl nsUrl { get; set; }
        public float Rate
        {
            get
            {
                if (Player != null)
                    return Player.Rate;
                return 0.0f;
            }
            set
            {
                if (Player != null)
                    Player.Rate = value;
            }
        }
        public TimeSpan Position
        {
            get
            {
                if (Player.CurrentItem == null)
                    return TimeSpan.Zero;
                return TimeSpan.FromMilliseconds(Player.CurrentItem.CurrentTime.Seconds);
            }
        }
        public TimeSpan Duration
        {
            get
            {
                if (Player.CurrentItem == null)
                    return TimeSpan.Zero;
                if (double.IsNaN(Player.CurrentItem.Duration.Seconds))
                    return TimeSpan.Zero;
                return TimeSpan.FromMilliseconds(Player.CurrentItem.Duration.Seconds);
            }
        }
        public TimeSpan Buffered
        {
            get
            {
                var buffered = TimeSpan.Zero;
                if (Player.CurrentItem != null)
                    buffered =
                        TimeSpan.FromMilliseconds(
                            Player.CurrentItem.LoadedTimeRanges.Select(
                                tr => tr.CMTimeRangeValue.Start.Seconds + tr.CMTimeRangeValue.Duration.Seconds).Max());

                Console.WriteLine("Buffered size: " + buffered);

                return buffered;
            }
        }
        public bool IsReadyRendering => RenderSurface != null && !RenderSurface.IsDisposed;
        public bool IsMuted
        {
            get { return _player.Muted; }
            set { 
                _player.Muted = value;
                VideoAudioVolumeChanged?.Invoke(this, new VolumeFileChangedEventArgs(value));
            }
        }

        public void SetVolume(float leftVolume, float rightVolume)
        {
            float volume = Math.Max(leftVolume, rightVolume);
            _player.Volume = volume;
        }
        public async Task Play()
        {
            Player.Play();
            Status = VideoStatus.Playing;
            await Task.CompletedTask;
        }
        public async Task Stop()
        {
            await Task.Run(() =>
            {
                if (Player.Rate != 0.0)
                    Player.Pause();

                Player.CurrentItem.Seek(CMTime.FromSeconds(0d, 1));

                Status = VideoStatus.Stopped;
            });
        }
        public async Task Pause()
        {
            await Task.Run(() =>
            {
                Status = VideoStatus.Paused;

                if (Player.Rate != 0.0)
                    Player.Pause();
            });
        }
        public VideoStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                VideoAudioStatusChanged?.Invoke(this, new StatusFileChangedEventArgs(_status));
            }
        }
        public Dictionary<string, string> RequestHeaders { get; set; }
        private bool _isfullscreen;
        public bool IsFullScreen
        {
            get { return _isfullscreen; }
            set
            {
                if (_isfullscreen != value)
                {
                    _isfullscreen = value;
                }
            }
        }

        public async Task Seek(TimeSpan position)
        {
            await Task.Run(() => { Player.CurrentItem?.Seek(CMTime.FromSeconds(position.TotalSeconds, 1)); });
        }
        private void InitializePlayer()
        {
            _player = new AVPlayer();
            _videoLayer = AVPlayerLayer.FromPlayer(_player);
           

#if __IOS__ || __TVOS__
            var avSession = AVAudioSession.SharedInstance();

            // By setting the Audio Session category to AVAudioSessionCategorPlayback, audio will continue to play when the silent switch is enabled, or when the screen is locked.
            avSession.SetCategory(AVAudioSessionCategory.Playback);

            NSError activationError = null;
            avSession.SetActive(true, out activationError);
            if (activationError != null)
                Console.WriteLine("Could not activate audio session {0}", activationError.LocalizedDescription);
#endif

            Player.AddPeriodicTimeObserver(new CMTime(1, 4), DispatchQueue.MainQueue, delegate
            {
                double totalProgress = 0;
                if (!double.IsNaN(_player.CurrentItem.Duration.Seconds))
                {
                    var totalDuration = TimeSpan.FromSeconds(_player.CurrentItem.Duration.Seconds);
                    totalProgress = Position.TotalSeconds /
                                        totalDuration.TotalSeconds;
                }

                if (Position.TotalSeconds > 0)
                    Element.Position = Position;
                if (Duration.TotalSeconds > 0)
                    Element.Duration = Duration;
                VideoAudioProgressChanged?.Invoke(this, new ProgressFileChangedEventArgs(
                    !double.IsInfinity(totalProgress) ? totalProgress : 0,
                    Position,
                    Duration));
            });
        }


        protected override void OnElementChanged(ElementChangedEventArgs<PXC_VideoPlayer> e)
        {
            base.OnElementChanged(e);
            var pxcvideo = (PXC_VideoPlayer)Element;
            InitControls();
            pxcvideo.PlayPauseCommand = new Command(async () =>
            {
                if (Status == VideoStatus.Playing)
                {
                    await Pause();
                }
                else
                {
                    await Play();
                }
            });
            pxcvideo.StopCommand = new Command(async () =>
            {
                await Stop();
            });
            pxcvideo.MuteCommand = new Command(() =>
            {
                IsMuted = !IsMuted;
            });
            pxcvideo.FullScreenCommand = new Command(() =>
            {
                IsFullScreen = !IsFullScreen;
                FullScren();
            });
        }
        public async void InitControls()
        {
            if (Control == null)
            {
                var pxcvideo = (PXC_VideoPlayer)Element;
               
                RenderSurface = new VideoSurface();

                SetNativeControl(RenderSurface);
                if (!string.IsNullOrEmpty(Element.FileSource))
                {
                    nsUrl = new NSUrl(Element.FileSource);
                }
                Status = VideoStatus.Loading;

                var nsAsset = AVUrlAsset.Create(nsUrl);
                var streamingItem = AVPlayerItem.FromAsset(nsAsset);

                _videoLayer.Frame = RenderSurface.Frame;
                _videoLayer.VideoGravity = AVLayerVideoGravity.ResizeAspect;
                RenderSurface.Layer.AddSublayer(_videoLayer);
                Player.CurrentItem?.RemoveObserver(this, new NSString("status"));

                Player.ReplaceCurrentItemWithPlayerItem(streamingItem);
                streamingItem.AddObserver(this, new NSString("status"), NSKeyValueObservingOptions.New, Player.Handle);
                streamingItem.AddObserver(this, new NSString("loadedTimeRanges"),
                    NSKeyValueObservingOptions.Initial | NSKeyValueObservingOptions.New, Player.Handle);

                Player.CurrentItem.SeekingWaitsForVideoCompositionRendering = true;
                Player.CurrentItem.AddObserver(this, (NSString)"status", NSKeyValueObservingOptions.New |
                                                                          NSKeyValueObservingOptions.Initial,
                    StatusObservationContext.Handle);

                NSNotificationCenter.DefaultCenter.AddObserver(AVPlayerItem.DidPlayToEndTimeNotification,
                                                               notification => VideoAudioFinishedChanged?.Invoke(this, new FinishedFileEventArgs(true)), Player.CurrentItem);
                if (Element.IsFullScreen)
                {
                    IsFullScreen = Element.IsFullScreen;
                    FullScren();
                }

                if (Element.SeekTo > 0)
                {
                    await Seek(TimeSpan.FromMilliseconds(Element.SeekTo));
                }

                if (Element.AutoPlay)
                {
                    await Play();
                }
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var video = sender as PXC_VideoPlayer;
            if (video != null)
            {
                InitControls();
            }
            if (e.PropertyName == PXC_VideoPlayer.FileSourceProperty.PropertyName)
            {
                nsUrl = new NSUrl(Element.FileSource);
            }
            else if (e.PropertyName == PXC_VideoPlayer.VolumeLevelProperty.PropertyName)
            {
                SetVolume((float)(Element.IsMuted ? 0 : Element.VolumeLevel), (float)(Element.IsMuted ? 0 : Element.VolumeLevel));
            }
            else if (e.PropertyName == PXC_VideoPlayer.PositionProperty.PropertyName)
            {
                Player.CurrentItem?.Seek(CMTime.FromSeconds(Element.Position.TotalMilliseconds, 1));
            }
        }
        public override void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
        {
            Console.WriteLine("Observer triggered for {0}", keyPath);

            switch ((string)keyPath)
            {
                case "status":
                    ObserveStatus();
                    return;

                case "loadedTimeRanges":
                    ObserveLoadedTimeRanges();
                    return;

                default:
                    Console.WriteLine("Observer triggered for {0} not resolved ...", keyPath);
                    return;
            }
        }
        private void ObserveStatus()
        {
            Console.WriteLine("Status Observed Method {0}", Player.Status);
            if ((Player.Status == AVPlayerStatus.ReadyToPlay) && (Status == VideoStatus.Buffering))
            {
                Status = VideoStatus.Playing;
                Player.Play();
            }
            else if (Player.Status == AVPlayerStatus.Failed)
            {
                OnMediaFailed();
                Status = VideoStatus.Stopped;
            }
        }
        private void OnMediaFailed()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Description: {Player.Error.LocalizedDescription}");
            builder.AppendLine($"Reason: {Player.Error.LocalizedFailureReason}");
            builder.AppendLine($"Recovery Options: {Player.Error.LocalizedRecoveryOptions}");
            builder.AppendLine($"Recovery Suggestion: {Player.Error.LocalizedRecoverySuggestion}");
            VideoAudioFailedChanged?.Invoke(this, new FailedFileEventArgs(builder.ToString(), new NSErrorException(Player.Error)));
        }
        private void ObserveLoadedTimeRanges()
        {
            var loadedTimeRanges = _player.CurrentItem.LoadedTimeRanges;
            if (loadedTimeRanges.Length > 0)
            {
                var range = loadedTimeRanges[0].CMTimeRangeValue;
                var duration = double.IsNaN(range.Duration.Seconds) ? TimeSpan.Zero : TimeSpan.FromSeconds(range.Duration.Seconds);
                var totalDuration = _player.CurrentItem.Duration;
                var bufferProgress = duration.TotalSeconds / totalDuration.Seconds;
                //VideoBufferingChanged?.Invoke(this, new BufferingChangedEventArgs(
                //    !double.IsInfinity(bufferProgress) ? bufferProgress : 0,
                //    duration
                //));
            }
            else
            {
                //VideoAudioBufferingChanged?.Invoke(this, new BufferingChangedEventArgs(0, TimeSpan.Zero));
            }
        }

        public void FullScren()
        {
           
        }

        public void ChangeOrientation(bool isfullscreen)
        {
           
        }
    }
}