using System;

namespace Plugin.Xamarin.Controls.EventArgsFile
{
    public class FinishedFileEventArgs : EventArgs
    {
        public FinishedFileEventArgs(bool finished)
        {
            Finished = finished;
        }

        public bool Finished { get; set; }
    }
}
