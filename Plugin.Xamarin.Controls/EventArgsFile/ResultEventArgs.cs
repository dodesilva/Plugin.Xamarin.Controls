using Plugin.Xamarin.Controls.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Xamarin.Controls.EventArgsFile
{
    public class ResultEventArgs : EventArgs
    {
        public ResultEventArgs(MediaFiles mediaFiles)
        {
            MediaFiles = mediaFiles;
        }
        public MediaFiles MediaFiles { get; set; }
    }
}
