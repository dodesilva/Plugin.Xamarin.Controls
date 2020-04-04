using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.EventArgsFile;
using Plugin.Xamarin.Controls.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_VideoPlayer : View
    {
        public PXC_VideoPlayer()
        {

        }

        public static readonly BindableProperty SeekToProperty =
            BindableProperty.Create(nameof(SeekTo), typeof(int), typeof(PXC_VideoPlayer), 0);
        public int SeekTo
        {
            get { return (int)GetValue(SeekToProperty); }
            set
            {
                SetValue(SeekToProperty, value);
            }
        }

        public static readonly BindableProperty FileSourceProperty =
            BindableProperty.Create(nameof(FileSource), typeof(string), typeof(PXC_VideoPlayer), string.Empty);
     
        public string FileSource
        {
            get { return (string)GetValue(FileSourceProperty); }
            set { SetValue(FileSourceProperty, value); }
        }

        public static readonly BindableProperty AutoPlayProperty =
            BindableProperty.Create(propertyName: nameof(AutoPlay), returnType: typeof(bool),
                declaringType: typeof(PXC_VideoPlayer), defaultValue: default(bool), defaultBindingMode: BindingMode.Default, defaultValueCreator: (o) => false);

        public bool AutoPlay
        {
            get { return (bool)GetValue(AutoPlayProperty); }
            set { SetValue(AutoPlayProperty, value); }
        }

        public static readonly BindableProperty IsFullScreenProperty =
            BindableProperty.Create(propertyName: nameof(IsFullScreen), returnType: typeof(bool),
                declaringType: typeof(PXC_VideoPlayer), defaultValue: default(bool), defaultBindingMode: BindingMode.Default, defaultValueCreator: (o) => false);

        public bool IsFullScreen
        {
            get { return (bool)GetValue(IsFullScreenProperty); }
            set { SetValue(IsFullScreenProperty, value); }
        }

        public static readonly BindableProperty ShowControlsProperty =
            BindableProperty.Create(propertyName: nameof(ShowControls), returnType: typeof(bool),
                declaringType: typeof(PXC_VideoPlayer), defaultValue: default(bool), defaultBindingMode: BindingMode.Default, defaultValueCreator: (o) => false);

        public bool ShowControls
        {
            get { return (bool)GetValue(ShowControlsProperty); }
            set { SetValue(ShowControlsProperty, value); }
        }

        public static readonly BindableProperty VolumeLevelProperty =
            BindableProperty.Create(propertyName: nameof(VolumeLevel), returnType: typeof(double),
                declaringType: typeof(PXC_VideoPlayer), defaultValue: 100D);

        public double VolumeLevel
        {
            get { return (double)GetValue(VolumeLevelProperty); }
            set { SetValue(VolumeLevelProperty, value); }
        }

        public static readonly BindableProperty IsMutedProperty =
            BindableProperty.Create(propertyName: nameof(IsMuted), returnType: typeof(bool),
                declaringType: typeof(PXC_VideoPlayer), defaultValue: false);

		public static readonly BindableProperty PositionProperty =
            BindableProperty.Create(propertyName: nameof(Position), returnType: typeof(TimeSpan),
                declaringType: typeof(PXC_VideoPlayer), defaultValue: new TimeSpan());

        public TimeSpan Position
        {
            get { return (TimeSpan)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }
       
		public static readonly BindableProperty DurationProperty =
            BindableProperty.Create(propertyName: nameof(Duration), returnType: typeof(TimeSpan),
                declaringType: typeof(PXC_VideoPlayer), defaultValue: new TimeSpan());

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public bool IsMuted
        {
            get { return (bool)GetValue(IsMutedProperty); }
            set { SetValue(IsMutedProperty, value); }
        }

        #region Commands
        // Play
        /// <summary>
        /// The play command property
        /// </summary>
        public static readonly BindableProperty PlayCommandProperty = BindableProperty.Create(nameof(PlayPauseCommand), typeof(ICommand), typeof(PXC_VideoPlayer));
        /// <summary>
        /// Gets the play command.
        /// </summary>
        /// <value>The play command.</value>
        public ICommand PlayPauseCommand { get { return (ICommand)GetValue(PlayCommandProperty); } set { SetValue(PlayCommandProperty, value); } }

        // Stop
        /// <summary>
        /// The stop command property
        /// </summary>
        public static readonly BindableProperty StopCommandProperty = BindableProperty.Create(nameof(StopCommand), typeof(ICommand), typeof(PXC_VideoPlayer));
        /// <summary>
        /// Gets the stop command.
        /// </summary>
        /// <value>The stop command.</value>
        public ICommand StopCommand { get { return (ICommand)GetValue(StopCommandProperty); } set { SetValue(StopCommandProperty, value); } }

        public static readonly BindableProperty MuteCommandProperty = BindableProperty.Create(nameof(MuteCommand), typeof(ICommand), typeof(PXC_VideoPlayer));
        /// <summary>
        /// Gets the mute command.
        /// </summary>
        /// <value>The mute command.</value>
        public ICommand MuteCommand { get { return (ICommand)GetValue(MuteCommandProperty); } set { SetValue(MuteCommandProperty, value); } }

        public static readonly BindableProperty FullScreenCommandProperty = BindableProperty.Create(nameof(FullScreenCommand), typeof(ICommand), typeof(PXC_VideoPlayer));
        /// <summary>
        /// Gets the mute command.
        /// </summary>
        /// <value>The mute command.</value>
        public ICommand FullScreenCommand { get { return (ICommand)GetValue(FullScreenCommandProperty); } set { SetValue(FullScreenCommandProperty, value); } }

       
        #endregion

        public event EventHandler<StatusFileChangedEventArgs> VideoStatusChanged;
        public event EventHandler<ProgressFileChangedEventArgs> VideoProgressChanged;
        public event EventHandler<FinishedFileEventArgs> VideoFinishedChanged;
        public event EventHandler<FailedFileEventArgs> VideoFailedChanged;
        public event EventHandler<VolumeFileChangedEventArgs> VideoVolumeChanged;
        public event EventHandler<bool> FullScreenStatusChanged;

        private IVideoPlayback Renderer = null;
        public void SetRenderer(IVideoPlayback renderer)
        {
            Renderer = renderer;
            
            Renderer.VideoAudioFailedChanged += Renderer_VideoAudioFailed;
            Renderer.VideoAudioFinishedChanged += Renderer_VideoAudioFinished;
            Renderer.VideoAudioProgressChanged += Renderer_VideoAudioPlayingChanged;
            Renderer.VideoAudioStatusChanged += Renderer_VideoAudioStatusChanged;
            Renderer.VideoAudioVolumeChanged += Renderer_VideoAudioVolume;
            Renderer.FullScreenStatusChanged += Renderer_FullScreenStatusChanged;
        }

        private void Renderer_FullScreenStatusChanged(object sender, bool e)
        {
            if (FullScreenStatusChanged != null)
            {
                FullScreenStatusChanged?.Invoke(this, e);
            }
        }

        private void Renderer_VideoAudioVolume(object sender, VolumeFileChangedEventArgs e)
        {
            if (VideoVolumeChanged != null)
            {
                VideoVolumeChanged?.Invoke(this, e);
            }
        }
        private void Renderer_VideoAudioStatusChanged(object sender, StatusFileChangedEventArgs e)
        {
            if (VideoStatusChanged != null)
            {
                VideoStatusChanged?.Invoke(this, e);
            }
        }
        private void Renderer_VideoAudioPlayingChanged(object sender, ProgressFileChangedEventArgs e)
        {
            if (VideoProgressChanged != null)
            {
                VideoProgressChanged?.Invoke(this, e);
            }
        }
        private void Renderer_VideoAudioFinished(object sender, FinishedFileEventArgs e)
        {
            VideoFinishedChanged?.Invoke(this, e);
        }
        private void Renderer_VideoAudioFailed(object sender, FailedFileEventArgs e)
        {
            VideoFailedChanged?.Invoke(this, e);
        }
        public async void Play()
        {
            await Renderer.Play();
        }
        public async void Pause()
        {
            await Renderer.Pause();
        }
        public async void Stop()
        {
            await Renderer.Stop();
        }
        public async void Seek(TimeSpan position)
        {
            await Renderer.Seek(position);
        } 
        public void OrientationChanged(bool islandorportrai)
        {
            Renderer.IsFullScreen = islandorportrai;
            Renderer.ChangeOrientation(islandorportrai);
        }
    }
}
