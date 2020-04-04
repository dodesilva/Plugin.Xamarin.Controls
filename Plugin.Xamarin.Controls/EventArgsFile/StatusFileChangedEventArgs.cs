using Plugin.Xamarin.Controls.EnumFiles;
using System;

namespace Plugin.Xamarin.Controls.EventArgsFile
{
    public class StatusFileChangedEventArgs:EventArgs
    {
        public StatusFileChangedEventArgs(VideoStatus status)
        {
            Status = status;
        }
        public VideoStatus Status { get; set; }
    }
}
