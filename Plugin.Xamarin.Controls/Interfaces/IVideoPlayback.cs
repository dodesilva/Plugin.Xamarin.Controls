using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.EventArgsFile;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Plugin.Xamarin.Controls.Interfaces
{
    public delegate void VideoAudioStatusChangedEventHandler(object sender, StatusFileChangedEventArgs e);
    public delegate void VideoAudioProgressChangedEventHandler(object sender, ProgressFileChangedEventArgs e);
    public delegate void VideoAudioFinishedEventHandler(object sender, FinishedFileEventArgs e);
    public delegate void VideoAudioFailedEventHandler(object sender, FailedFileEventArgs e);
    public delegate void VideoAudioVolumeEventHandler(object sender, VolumeFileChangedEventArgs e);
    public interface IVideoPlayback
    {
        TimeSpan Position { get; }
        TimeSpan Duration { get; }
        TimeSpan Buffered { get; }
        bool IsMuted { get; set; }
        bool IsFullScreen { get; set; }
        VideoStatus Status { get; set; }
        event VideoAudioStatusChangedEventHandler VideoAudioStatusChanged;
        event VideoAudioProgressChangedEventHandler VideoAudioProgressChanged;
        event VideoAudioFinishedEventHandler VideoAudioFinishedChanged;
        event VideoAudioFailedEventHandler VideoAudioFailedChanged;
        event VideoAudioVolumeEventHandler VideoAudioVolumeChanged;
        event EventHandler<bool> FullScreenStatusChanged;      
        Task Play();
        Task Pause();
        Task Stop();
        Task Seek(TimeSpan position);
        void FullScren();
        void ChangeOrientation(bool isfullscreen);
    }
}
