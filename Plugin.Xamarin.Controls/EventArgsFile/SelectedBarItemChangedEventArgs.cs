using Plugin.Xamarin.Controls.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Xamarin.Controls.EventArgsFile
{
    public class SelectedBarItemChangedEventArgs:EventArgs
    {
        public int CurrentBar { get; set; }
        public SelectedBarItemChangedEventArgs(int _currentBar)
        {
            CurrentBar = _currentBar;
        }
    }
}
