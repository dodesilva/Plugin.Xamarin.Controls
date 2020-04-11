using Plugin.Xamarin.Controls.MultySelectable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_LIstView:ListView
    { 
        public event EventHandler LoadMoreUpChanged;
        public event EventHandler LoadMoreDownChanged;
        #region Bindables
         public static readonly BindableProperty LoadMoreUpCommandProperty =BindableProperty.Create("LoadMoreUpCommand", typeof(ICommand), typeof(PXC_LIstView), default(ICommand));
        public static readonly BindableProperty LoadMoreDownCommandProperty = BindableProperty.Create("LoadMoreDownCommand", typeof(ICommand), typeof(PXC_LIstView), default(ICommand));
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
       
        #endregion
        public PXC_LIstView()
        {
            ItemAppearing += PXC_LIstView_ItemAppearing;
            ItemSelected += PXC_LIstView_ItemSelected;
        }

        private void PXC_LIstView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private int _lastItemAppearedIdx = 0;
        private void PXC_LIstView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var items = ItemsSource as IList;

            if (e.ItemIndex < _lastItemAppearedIdx)
            {
                if (e.ItemIndex < 1)
                {
                    if (LoadMoreUpCommand != null && LoadMoreUpCommand.CanExecute(null))
                        LoadMoreUpCommand.Execute(null);
                    if (LoadMoreUpChanged != null)
                        LoadMoreUpChanged?.Invoke(sender, e);
                }
            }
            else
            {

                if (items != null && e.Item == items[items.Count - 1])
                {
                    if (LoadMoreDownCommand != null && LoadMoreDownCommand.CanExecute(null))
                        LoadMoreDownCommand.Execute(null);
                    if (LoadMoreDownChanged != null)
                        LoadMoreDownChanged?.Invoke(sender, e);
                }
            }
            _lastItemAppearedIdx = e.ItemIndex;
        }
    }
}
