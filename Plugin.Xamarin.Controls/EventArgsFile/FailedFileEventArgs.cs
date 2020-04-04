using System;

namespace Plugin.Xamarin.Controls.EventArgsFile
{
    public class FailedFileEventArgs : EventArgs
    {
        public FailedFileEventArgs(string description, Exception exception)
        {
            Description = description;
            Exception = exception;
        }

        public Exception Exception { get; set; }
        public string Description { get; set; }
    }
}
