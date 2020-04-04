using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Xamarin.Controls.EventArgsFile
{
    public class VolumeFileChangedEventArgs : EventArgs
    {
        public VolumeFileChangedEventArgs( bool muted)
        {
            Muted = muted;
        }

        public bool Muted { get; }
    }
}
