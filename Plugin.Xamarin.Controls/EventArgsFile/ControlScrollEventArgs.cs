using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Xamarin.Controls.EventArgsFile
{
    public class ControlScrollEventArgs : EventArgs
    {
        /// <summary>
        /// The delta.
        /// </summary>
        public float Delta { get; set; }

        /// <summary>
        /// The current vertical position.
        /// </summary>
        public float CurrentY { get; set; }

        /// <summary>
        /// Initialize a new instance of the ControlScrollEventArgs
        /// </summary>
        /// <param name="delta">The delta</param>
        /// <param name="currentY">The current vertical position.</param>
        public ControlScrollEventArgs(float delta, float currentY)
        {
            this.Delta = delta;
            this.CurrentY = currentY;
        }
    }
}
