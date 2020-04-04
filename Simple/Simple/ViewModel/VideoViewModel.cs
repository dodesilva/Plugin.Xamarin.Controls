using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.EventArgsFile;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Simple.ViewModel
{
    public class VideoViewModel:BaseViewModel
    {
        #region Attributos
        private string _volumeImage;
        private double _Volume = 1;
        private bool _Muted = false;
        private string _Duration;
        private string _Position;
        private double _totalMilliseconds=1;
        private double _progress;
        private string _Source;
        private string _playPauseImage;
        private int _positionvalue;
        bool isdraging = false;
        #endregion
        #region Propriedades

        public string PerfilName { get; set; }
        public string ImagePerfile { get; set; }
        public string PlayPauseImage
        {
            get { return _playPauseImage; }
            set { SetValue(ref _playPauseImage, value); }
        }

        public string VolumeImage
        {
            get { return _volumeImage; }
            set { SetValue(ref _volumeImage, value); }
        }


        public double Volume
        {
            get { return _Volume; }
            set
            {
                _Volume = value;

                OnPropertyChanged(nameof(Volume));
                OnPropertyChanged(nameof(SliderVolume));
            }
        }
        public double SliderVolume
        {
            get { return _Volume * 100; }
            set
            {
                try
                {
                    _Volume = value / 100;
                }
                catch
                {
                    _Volume = 0;
                }

                OnPropertyChanged(nameof(Volume));
                OnPropertyChanged(nameof(SliderVolume));
            }
        }
        public bool Muted
        {
            get { return _Muted; }
            set { SetValue(ref _Muted, value); }
        }
        
        public string Duration
        {
            get { return _Duration; }
            set { SetValue(ref _Duration, value); }
        }

        public double SliderDuration
        {
            get{ return _totalMilliseconds;}
            set { SetValue(ref _totalMilliseconds, value); }
        }

        public string Position
        {
            get { return _Position; }
            set
            {
                SetValue(ref _Position, value);
            }
        }

        public int SliderPosition
        {
            get { return _positionvalue; }
            set
            {
                 _positionvalue= value;
               
            }
        }

        public string VideoSource
        {
            get{ return _Source; }
            set { SetValue(ref _Source, value); }
        }
        #endregion
       
        public VideoViewModel()
        {
            VideoSource = "https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";
            PlayPauseImage = "ic_pause_circle_outline.png";
            VolumeImage = "VolumeHigh";
        }

        #region Commands
        public ICommand VolumeStatusChangedCommand
        {
            get
            {
                return new Command<bool>((e) =>
                {
                    if (e)
                    {
                        VolumeImage = "VolumeHigh";
                    }
                    else
                    {
                        VolumeImage = "VolumeMute2";
                    }
                });
            }
        }
        public ICommand PropertyChangedCommand
        {
            get
            {
                return new Command<ProgressFileChangedEventArgs>((e) =>
                {
                    SliderPosition = (int)e.Progress;
                    Duration = GetFormattedTime((int)e.Duration.TotalMilliseconds);
                    Position = GetFormattedTime((int)e.Position.TotalMilliseconds);
                    if (e.Duration.TotalSeconds > 0)                  
                    {
                        SliderDuration = e.Duration.TotalMilliseconds;
                    }
                    OnPropertyChanged(nameof(SliderPosition));
                });
            }
        }
        public ICommand StatusChangedCommand
        {
            get
            {
                return new Command<StatusFileChangedEventArgs>((e) =>
                {
                    switch (e.Status)
                    {
                        case VideoStatus.Loading:
                            PlayPauseImage = "ic_play_circle_outline.png";
                            break;
                        case VideoStatus.Paused:
                            PlayPauseImage = "ic_play_circle_outline.png";
                            break;
                        case VideoStatus.Playing:
                            PlayPauseImage = "ic_pause_circle_outline.png";
                            break;
                        case VideoStatus.Stopped:
                            PlayPauseImage = "ic_play_circle_outline.png";
                            break;
                    }
                    OnPropertyChanged(nameof(PlayPauseImage));
                });
            }
        }
        #endregion
        public string GetFormattedTime(int value)
        {
            var span = TimeSpan.FromMilliseconds(value);
            if (span.Hours > 0)
            {
                return string.Format("{0}:{1:00}:{2:00}", (int)span.TotalHours, span.Minutes, span.Seconds);
            }
            else
            {
                return string.Format("{0}:{1:00}", (int)span.Minutes, span.Seconds);
            }
        }
    }
}
