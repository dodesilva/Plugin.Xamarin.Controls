using Plugin.Xamarin.Controls.MultySelectable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_CollectionView:CollectionView
    { 
        public event EventHandler LoadMoreUpChanged;
        public event EventHandler LoadMoreDownChanged;
        #region Bindables
        public static readonly BindableProperty LoadMoreUpCommandProperty = BindableProperty.Create("LoadMoreUpCommand", typeof(ICommand), typeof(PXC_CollectionView), default(ICommand));
        public static readonly BindableProperty LoadMoreDownCommandProperty = BindableProperty.Create("LoadMoreDownCommand", typeof(ICommand), typeof(PXC_CollectionView), default(ICommand));
        public static readonly BindableProperty IsHorizontalProperty = BindableProperty.Create("IsHorizontal", typeof(bool), typeof(PXC_CollectionView), false);
        #endregion
        #region Property
       
        public ICommand LoadMoreUpCommand
        {
            get { return (ICommand)GetValue(LoadMoreUpCommandProperty); }
            set { SetValue(LoadMoreUpCommandProperty, value); }
        }
        public ICommand LoadMoreDownCommand
        {
            get { return (ICommand)GetValue(LoadMoreDownCommandProperty); }
            set { SetValue(LoadMoreDownCommandProperty, value); }
        }
        public bool IsHorizontal
        {
            get { return (bool)GetValue(IsHorizontalProperty); }
            set { SetValue(IsHorizontalProperty, value); }
        }
        #endregion
        public PXC_CollectionView()
        {
            Scrolled += InfiniteCollectionScroll;
        }
        private void InfiniteCollectionScroll(object sender, ItemsViewScrolledEventArgs e)
        {
            var items = ItemsSource as IList;
            if (items != null)
            {
                if (IsHorizontal)
                {
                    if (e.HorizontalDelta < 0)
                    {
                        if (e.FirstVisibleItemIndex == _lastItemAppearedIdx)
                        {
                            if (LoadMoreUpCommand != null && LoadMoreUpCommand.CanExecute(null))
                                LoadMoreUpCommand.Execute(null);
                            if (LoadMoreUpChanged != null)
                                LoadMoreUpChanged?.Invoke(sender, e);
                        }
                    }
                    else
                    {

                        if (e.HorizontalDelta > 0)
                        {
                            if (e.LastVisibleItemIndex == items.Count - 1)
                            {
                                if (LoadMoreDownCommand != null && LoadMoreDownCommand.CanExecute(null))
                                    LoadMoreDownCommand.Execute(null);
                                if (LoadMoreDownChanged != null)
                                    LoadMoreDownChanged?.Invoke(sender, e);
                            }
                        }
                    }
                    
                }
                else
                {
                    if (e.VerticalDelta < 0)
                    {
                        if (e.FirstVisibleItemIndex == _lastItemAppearedIdx)
                        {
                            if (LoadMoreUpCommand != null && LoadMoreUpCommand.CanExecute(null))
                                LoadMoreUpCommand.Execute(null);
                            if (LoadMoreUpChanged != null)
                                LoadMoreUpChanged?.Invoke(sender, e);
                        }
                    }
                    else
                    {

                        if (e.VerticalDelta > 0)
                        {
                            if (e.LastVisibleItemIndex == items.Count - 1)
                            {
                                if (LoadMoreDownCommand != null && LoadMoreDownCommand.CanExecute(null))
                                    LoadMoreDownCommand.Execute(null);
                                if (LoadMoreDownChanged != null)
                                    LoadMoreDownChanged?.Invoke(sender, e);
                            }
                        }
                    }
                } 
                _lastItemAppearedIdx = e.FirstVisibleItemIndex;
            }
        }
        private int _lastItemAppearedIdx = 0;
    }
}
