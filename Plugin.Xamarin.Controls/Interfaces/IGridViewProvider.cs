using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Xamarin.Controls.Interfaces
{
    public interface IGridViewProvider
    {
        /// <summary>
        /// Repopulate the grid view with items.
        /// </summary>
		void ReloadData();

        /// <summary>
        /// Scroll to the item at the specified index.
        /// </summary>
        /// <param name="index">The index to scroll to.</param>
        /// <param name="animated">Whether the scrolling should be animated.</param>
		void ScrollToItemWithIndex(int index, bool animated);
    }
}
