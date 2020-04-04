using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Xamarin.Controls.EventArgsFile
{
    public class ItemHoldEventArgs:EventArgs
    {
        public ItemHoldEventArgs(object item)
        {
            Item = item;
        }

        public object Item { get; }
    }
}
