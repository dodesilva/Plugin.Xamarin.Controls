using Plugin.Xamarin.Controls.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Xamarin.Controls.EventArgsFile
{
    public class PopupDataChangedEventArgs:EventArgs
    {
        public DataModel DataModel { get; set; }
        public PopupDataChangedEventArgs(DataModel data)
        {
            DataModel = data;
        }
    }
}
