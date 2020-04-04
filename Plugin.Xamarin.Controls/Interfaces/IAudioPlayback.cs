using Plugin.Xamarin.Controls.EnumFiles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Xamarin.Controls.Interfaces
{
    public interface IAudioPlayback
    {
        TimeSpan Position { get; }
        TimeSpan Duration { get; }
        TimeSpan Buffered { get; }
        bool IsMuted { get; set; }
        VideoStatus Status { get; set; }
        event VideoAudioStatusChangedEventHandler VideoAudioStatusChanged;
        event VideoAudioProgressChangedEventHandler VideoAudioProgressChanged;
        event VideoAudioFinishedEventHandler VideoAudioFinishedChanged;
        event VideoAudioFailedEventHandler VideoAudioFailedChanged;
        event VideoAudioVolumeEventHandler VideoAudioVolumeChanged;
        Task Play();
        Task Pause();
        Task Stop();
        Task Seek(TimeSpan position);
    }
}
