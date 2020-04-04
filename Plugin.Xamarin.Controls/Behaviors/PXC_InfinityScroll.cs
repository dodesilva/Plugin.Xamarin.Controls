using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls.Behaviors
{
    public class PXC_InfinityScroll : Behavior<ListView>
    {
        public static readonly BindableProperty LoadMoreUpCommandProperty =
               BindableProperty.Create("LoadMoreUpCommand", typeof(ICommand), typeof(PXC_InfinityScroll), default(ICommand));

        public ICommand LoadMoreUpCommand
        {
            get { return (ICommand)GetValue(LoadMoreUpCommandProperty); }
            set { SetValue(LoadMoreUpCommandProperty, value); }
        }

        public event EventHandler LoadMoreUpChanged;


        public static readonly BindableProperty LoadMoreDownCommandProperty =
              BindableProperty.Create("LoadMoreDownCommand", typeof(ICommand), typeof(PXC_InfinityScroll), default(ICommand));

        public ICommand LoadMoreDownCommand
        {
            get { return (ICommand)GetValue(LoadMoreDownCommandProperty); }
            set { SetValue(LoadMoreDownCommandProperty, value); }
        }

        public event EventHandler LoadMoreDownChanged;

        public ListView AssociatedObject
        {
            get;
            private set;
        }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += Bindable_BindingContextChanged;
            bindable.ItemAppearing += InfiniteListView_ItemAppearing;
        }

        private void Bindable_BindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= Bindable_BindingContextChanged;
            bindable.ItemAppearing -= InfiniteListView_ItemAppearing;
        }

        private int _lastItemAppearedIdx = 0;
        void InfiniteListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

            var items = AssociatedObject.ItemsSource as IList;

            //var currentItem = items.IndexOf(e.Item)-1;

            if (e.ItemIndex < _lastItemAppearedIdx)
            {
                if (e.ItemIndex < 1)
                {
                    if (LoadMoreUpCommand != null && LoadMoreUpCommand.CanExecute(null))
                        LoadMoreUpCommand.Execute(null);

                    LoadMoreUpChanged?.Invoke(sender, e);
                }
            }
            else
            {

                if (items != null && e.Item == items[items.Count - 1])
                {
                    if (LoadMoreDownCommand != null && LoadMoreDownCommand.CanExecute(null))
                        LoadMoreDownCommand.Execute(null);

                    LoadMoreDownChanged?.Invoke(sender, e);

                }
            }
            _lastItemAppearedIdx = e.ItemIndex;
        }
    }
}
