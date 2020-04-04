using System;

namespace Plugin.Xamarin.Controls.EventArgsFile
{
    public class ProgressFileChangedEventArgs:EventArgs
    {
        public ProgressFileChangedEventArgs(double progress, TimeSpan position, TimeSpan duration)
        {
            Progress = progress;
            Position = position;
            Duration = duration;
        }
        public double Progress { get; }
        public TimeSpan Position { get; }
        public TimeSpan Duration { get; }
    }
}
