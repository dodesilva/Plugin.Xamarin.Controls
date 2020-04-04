using Plugin.Xamarin.Controls.EventArgsFile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Xamarin.Controls.Interfaces
{
    public interface IScrollAwareElement
    {
        /// <summary>
        /// When the scrolling begins.
        /// </summary>
        event EventHandler OnStartScroll;

        /// <summary>
        /// When the scrolling ends.
        /// </summary>
        event EventHandler OnStopScroll;

        /// <summary>
        /// When the list is scrolled.
        /// </summary>
        event EventHandler<ControlScrollEventArgs> OnScroll;

        /// <summary>
        /// Raise the on scroll event.
        /// </summary>
        /// <param name="delta">The delta.</param>
        /// <param name="currentY">The current position.</param>
        void RaiseOnScroll(float delta, float currentY);

        /// <summary>
        /// Raise the start scroll event.
        /// </summary>
        void RaiseOnStartScroll();

        /// <summary>
        /// Raise the stop scroll event.
        /// </summary>
        void RaiseOnStopScroll();
    }
}
