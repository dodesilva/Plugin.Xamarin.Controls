using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_RepeaterView : StackLayout
    {
        public event EventHandler<ItemTappedEventArgs> ItemSelected;

        public static readonly BindableProperty HeaderTemplateProperty =
               BindableProperty.Create(nameof(HeaderTemplate), typeof(DataTemplate), typeof(PXC_RepeaterView), default(DataTemplate));

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(PXC_RepeaterView), default(DataTemplate));

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(ICollection), typeof(PXC_RepeaterView),
                null, defaultBindingMode: BindingMode.OneWay, propertyChanged: ItemsChanged);

        public static readonly BindableProperty SelectedCommandProperty =
            BindableProperty.Create(nameof(SelectedCommand), typeof(ICommand), typeof(PXC_RepeaterView), null, BindingMode.OneWay);
        
        public ICommand SelectedCommand
        {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty =
           BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(PXC_RepeaterView), default(object), BindingMode.OneWay);

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public PXC_RepeaterView()
        {
            Spacing = 0;
        }

        public ICollection ItemsSource
        {
            get { return (ICollection)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        protected virtual View ViewFor(object item)
        {
            View view = null;
            if (ItemTemplate != null)
            {
                var content = ItemTemplate.CreateContent();
                view = (content is View) ? content as View : ((ViewCell)content).View;

                view.BindingContext = item;

                var command = SelectedCommand ?? new Command((obj) =>
                {
                    var args = new ItemTappedEventArgs(ItemsSource, item);
                    ItemSelected?.Invoke(this, args);
                });
                var commandParameter = CommandParameter ?? item;
                view.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = command,
                    CommandParameter = commandParameter,
                    NumberOfTapsRequired = 1
                });
            }

            return view;
        }
        
        protected View HeaderView()
        {
            View view = null;

            if (HeaderTemplate != null)
            {
                var content = HeaderTemplate.CreateContent();
                view = (content is View) ? content as View : ((ViewCell)content).View;
                view.BindingContext = this.BindingContext;
            }

            return view;
        }

        private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as PXC_RepeaterView;
            if (control == null)
                return;

            control.Children.Clear();

            var items = (ICollection)newValue;
            if (items == null) return;
            var header = control.HeaderView();
            if (header != null)
                control.Children.Add(header);

            foreach (var item in items)
                control.Children.Add(control.ViewFor(item));

        }
    }
}
