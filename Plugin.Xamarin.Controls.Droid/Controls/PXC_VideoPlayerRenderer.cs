using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Media;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Util.Concurrent;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Droid.Classes;
using Plugin.Xamarin.Controls.Droid.Controls;
using Plugin.Xamarin.Controls.Droid.Listner;
using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.EventArgsFile;
using Plugin.Xamarin.Controls.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Math = System.Math;
using URL = Android.Net.Uri;

[assembly: ExportRenderer(typeof(PXC_VideoPlayer), typeof(PXC_VideoPlayerRenderer))]
namespace Plugin.Xamarin.Controls.Droid.Controls
{
    public class PXC_VideoPlayerRenderer : ViewRenderer<PXC_VideoPlayer, VideoSurface>, IVideoPlayback, MediaPlayer.IOnCompletionListener,
        MediaPlayer.IOnErrorListener, MediaPlayer.IOnPreparedListener, MediaPlayer.IOnInfoListener
    {
        Context _context;
        private VideoSurface RenderSurface;
        private MediaPlayer _mediaPlayer;
        private AudioManager _audioManager = null;
        public PXC_VideoPlayerRenderer(Context context) : base(context)
        {
            _context = context;
            _audioManager = (AudioManager)context.GetSystemService(Context.AudioService);
            _status = VideoStatus.Stopped;
            VideoAudioStatusChanged += (sender, args) => OnPlayingHandler(args);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<PXC_VideoPlayer> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                var pxcvideo = (PXC_VideoPlayer)Element;
                
                if (Control == null)
                {
                    pxcvideo.SetRenderer(this);
                    RenderSurface = new VideoSurface(_context);

                    SetNativeControl(RenderSurface);
                    Init();
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
                    pxcvideo.StopCommand = new Command(async() =>
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
            }
        }
        
       
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
           
            if (e.PropertyName == PXC_VideoPlayer.FileSourceProperty.PropertyName)
            {
                RenderSurface.SetVideoURI(global::Android.Net.Uri.Parse(Element.FileSource));
            }
            else if (e.PropertyName == PXC_VideoPlayer.ShowControlsProperty.PropertyName)
            {
                if (Element.ShowControls)
                {
                    var mediaController = new MediaController(((VideoView)RenderSurface).Context);
                    mediaController.SetAnchorView(RenderSurface);
                    RenderSurface.SetMediaController(mediaController);
                }
            }else if (e.PropertyName == PXC_VideoPlayer.VolumeLevelProperty.PropertyName)
            {
                SetVolume((float)(Element.IsMuted ? 0 : Element.VolumeLevel), (float)(Element.IsMuted ? 0 : Element.VolumeLevel));
            }
            else if (e.PropertyName == PXC_VideoPlayer.PositionProperty.PropertyName)
            {
                if (Math.Abs(RenderSurface.CurrentPosition - Element.Position.TotalMilliseconds) > 1000)
                {
                    RenderSurface.SeekTo((int)Element.Position.TotalMilliseconds);
                }
            }
            //if (Element.IsFullScreen)
            //{
            //    FullScren(Element.IsFullScreen);
            //}
        }
        private IScheduledExecutorService _executorService = Executors.NewSingleThreadScheduledExecutor();
        private IScheduledFuture _scheduledFuture;

        private void OnPlayingHandler(StatusFileChangedEventArgs args)
        {
            if (args.Status == VideoStatus.Playing)
            {
                CancelPlayingHandler();
                StartPlayingHandler();
            }
            if (args.Status == VideoStatus.Stopped || args.Status == VideoStatus.Error || args.Status == VideoStatus.Paused)
                CancelPlayingHandler();
        }
        private void CancelPlayingHandler()
        {
            _scheduledFuture?.Cancel(false);
        }
        private void StartPlayingHandler()
        {
            var handler = new Handler();
            var runnable = new Runnable(() => { handler.Post(OnPlaying); });
            if (!_executorService.IsShutdown)
            {
                _scheduledFuture = _executorService.ScheduleAtFixedRate(runnable, 100, 1000, TimeUnit.Milliseconds);
            }
        }
        public bool IsReadyRendering => RenderSurface != null && !RenderSurface.IsDisposed;
        private void OnPlaying()
        {
            if (!IsReadyRendering)
                CancelPlayingHandler();

            var progress = (Position.TotalSeconds / Duration.TotalSeconds);
            var position = Position;
            var duration = Duration;
            if (position.TotalSeconds > 0)
                Element.Position = position;
            if (duration.TotalSeconds > 0)
                Element.Duration = duration;
            VideoAudioProgressChanged?.Invoke(this, new ProgressFileChangedEventArgs(!double.IsInfinity(progress) ? progress : 0,
                position.TotalSeconds >= 0 ? position : TimeSpan.Zero,duration.TotalSeconds >= 0 ? duration : TimeSpan.Zero));
        }
        private VideoStatus _status = VideoStatus.Stopped;
        public VideoStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnStatusChanged(new StatusFileChangedEventArgs(_status));
            }
        }
        public void OnStatusChanged(StatusFileChangedEventArgs e)
        {
            VideoAudioStatusChanged?.Invoke(this, e);
        }

        public void OnProgressChanged(ProgressFileChangedEventArgs e)
        {
            VideoAudioProgressChanged?.Invoke(this, e);
        }

        public void OnFinishedFile(FinishedFileEventArgs e)
        {
            VideoAudioFinishedChanged?.Invoke(this, e);
        }

        public void OnFailedFile(FailedFileEventArgs e)
        {
            VideoAudioFailedChanged?.Invoke(this, e);
        }

        public void OnVolumeChanged(bool ismute)
        {
            VideoAudioVolumeChanged?.Invoke(this, new VolumeFileChangedEventArgs(ismute));
        }
        public TimeSpan Buffered => IsReadyRendering == false ? TimeSpan.Zero : TimeSpan.FromMilliseconds(RenderSurface.BufferPercentage);
        public TimeSpan Duration => IsReadyRendering == false ? TimeSpan.Zero : TimeSpan.FromMilliseconds(RenderSurface.Duration);
        public TimeSpan Position => IsReadyRendering == false ? TimeSpan.Zero : TimeSpan.FromMilliseconds(RenderSurface.CurrentPosition);

        private bool _IsMuted = false;
        public bool IsMuted
        {
            get { return _IsMuted; }
            set
            {
                if (_IsMuted == value)
                    return;

                float volumeValue = 0.0f;
                if (!value)
                {
                    //https://developer.xamarin.com/api/member/Android.Media.AudioManager.GetStreamVolume/p/Android.Media.Stream/
                    //https://stackoverflow.com/questions/17898382/audiomanager-getstreamvolumeaudiomanager-stream-music-returns-0
                    Stream streamType = Stream.Music;
                    int volumeMax = _audioManager.GetStreamMaxVolume(streamType);
                    int volume = _audioManager.GetStreamVolume(streamType);

                    //ltang: Unmute with the current volume
                    volumeValue = (float)volume / volumeMax;
                }

                SetVolume(volumeValue, volumeValue);
                _IsMuted = value;
                OnVolumeChanged(value);
            }
        }

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

        public void SetVolume(float leftVolume, float rightVolume)
        {
            try
            {
                _mediaPlayer?.SetVolume(leftVolume, rightVolume);
            }
            catch (Java.Lang.IllegalStateException e)
            {
                //ltang: Wrong state to set volume
                throw;
            }
            catch (System.Exception e)
            {
                throw;
            }
        }

        public event VideoAudioStatusChangedEventHandler VideoAudioStatusChanged;
        public event VideoAudioProgressChangedEventHandler VideoAudioProgressChanged;
        public event VideoAudioFinishedEventHandler VideoAudioFinishedChanged;
        public event VideoAudioFailedEventHandler VideoAudioFailedChanged;
        public event VideoAudioVolumeEventHandler VideoAudioVolumeChanged;

        public event EventHandler<bool> FullScreenStatusChanged;

        public async void Init()
        {
            Status = VideoStatus.Loading;
            var pxcvideo = (PXC_VideoPlayer)Element;
            if (pxcvideo.ShowControls)
            {
                var mediaController = new MediaController(((VideoView)RenderSurface).Context);
                mediaController.SetAnchorView(RenderSurface);
                RenderSurface.SetMediaController(mediaController);
            }

            RenderSurface.SetOnCompletionListener(this);
            RenderSurface.SetOnErrorListener(this);
            RenderSurface.SetOnPreparedListener(this);
            RenderSurface.SetOnInfoListener(this);
            if (!string.IsNullOrEmpty(Element.FileSource))
            {
                RenderSurface.SetVideoURI(URL.Parse(Element.FileSource));
            }

            if (Element.IsFullScreen)
            {
                IsFullScreen = Element.IsFullScreen;
                FullScren();
            }

            if (Element.AutoPlay)
            {
                await Play();
            }

            if (Element.SeekTo > 0)
            {
                await Seek(TimeSpan.FromMilliseconds(Element.SeekTo));
            }
        }

        public async Task Pause()
        {
            if (Status == VideoStatus.Playing)
            {
                RenderSurface.Pause();
                Status = VideoStatus.Paused;
            }
            await Task.CompletedTask;
        }
        public async Task Play()
        {
            if (!IsReadyRendering)
                return;

            RenderSurface.Start();
            Status = VideoStatus.Playing;
            await Task.CompletedTask;
        }
        public async Task Seek(TimeSpan position)
        {
            RenderSurface.SeekTo((int)position.TotalMilliseconds);
            await Task.CompletedTask;
        }

        public async Task Stop()
        {
            RenderSurface.StopPlayback();
            Status = VideoStatus.Stopped;
            await Task.CompletedTask;
        }
        public void OnCompletion(MediaPlayer mp)
        {
            VideoAudioFinishedChanged?.Invoke(this, new FinishedFileEventArgs(true));
            Status = VideoStatus.Finished;
        }

        public bool OnError(MediaPlayer mp, [GeneratedEnum] MediaError what, int extra)
        {
            Stop().Wait();
            Status = VideoStatus.Error;
            VideoAudioFailedChanged?.Invoke(this, new FailedFileEventArgs(what.ToString(), new System.Exception()));
            return true;
        }

        public void OnPrepared(MediaPlayer mp)
        {
            _mediaPlayer = mp;
            //Status = VideoStatus.Loading;
        }

        public bool OnInfo(MediaPlayer mp, MediaInfo what, int extra)
        {
            switch (what)
            {
                case MediaInfo.BadInterleaving:
                    break;
                case MediaInfo.BufferingEnd:
                    break;
                case MediaInfo.BufferingStart:
                    break;
                case MediaInfo.MetadataUpdate:
                    break;
                case MediaInfo.NotSeekable:
                    break;
                case MediaInfo.SubtitleTimedOut:
                    break;
                case MediaInfo.Unknown:
                    break;
                case MediaInfo.UnsupportedSubtitle:
                    break;
                case MediaInfo.VideoRenderingStart:
                    break;
                case MediaInfo.VideoTrackLagging:
                    break;
            }

            return true;
        }
        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            var a = this.Context as Activity;

            var p = RenderSurface.LayoutParameters;
            int width = MeasureSpec.GetSize(widthMeasureSpec);
            int height = MeasureSpec.GetSize(heightMeasureSpec);
            p.Height = height;
            p.Width = width;
           
            RenderSurface.LayoutParameters = p;
            
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }

        public void FullScren()
        {
            var activity = _context as Activity;
            if (IsFullScreen)
            {
                activity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;
                var window = activity.Window;
                window.AddFlags(WindowManagerFlags.Fullscreen);
            }
            else
            {
                activity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
                var window = activity.Window;
                window.ClearFlags(WindowManagerFlags.Fullscreen);
                Task.Delay(3000);
                activity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Unspecified;
            }
            FullScreenStatusChanged?.Invoke(this, IsFullScreen);
        }
        public void ChangeOrientation(bool isfullscreen)
        {
            var activity = _context as Activity;

            if (isfullscreen)
            {
                //activity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Locked;
                var window = activity.Window;
                window.AddFlags(WindowManagerFlags.Fullscreen);
            }
            else
            {
                //activity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Unspecified;
                var window = activity.Window;
                window.ClearFlags(WindowManagerFlags.Fullscreen);
            }
        }
    }
}