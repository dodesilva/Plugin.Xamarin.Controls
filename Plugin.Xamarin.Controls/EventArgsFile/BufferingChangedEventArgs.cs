using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Xamarin.Controls.EventArgsFile
{
    public class BufferingChangedEventArgs : EventArgs
    {
        public BufferingChangedEventArgs(double bufferProgress, TimeSpan bufferedTime)
        {
            BufferProgress = bufferProgress;
            BufferedTime = bufferedTime;
        }
        public double BufferProgress { get; }
        public TimeSpan BufferedTime { get; }
    }
}
