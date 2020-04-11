using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Xamarin.Controls.EventArgsFile
{
    public class CheckChangedArgs : EventArgs
    {
        public CheckChangedArgs(bool _isChecked, object _item)
        {
            IsChecked = _isChecked;
            Items = _item;
        }
        public bool IsChecked { get; set; }
        public object Items { get; set; }
    }
}
