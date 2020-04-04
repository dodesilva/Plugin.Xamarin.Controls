using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls.Helpers
{
    public class MediaFiles
    {
        public string FullPath { get; set; }
        public ImageSource FileSource { get; set; }
        public byte[] ContentByte { get; set; }
        public bool Success { get; set; }
        public string MimeType { get; set; }

    }
}
