using System;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_Picker:Picker
    {
        Boolean _disableNestedCalls;

        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(PXC_Picker),
                null, propertyChanged: OnItemsSourceChanged);
        
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(Object), typeof(PXC_Picker),
                null, BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);
        
        public static readonly BindableProperty BorderWidthProperty =
             BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(PXC_Picker), 0);

        public static readonly BindableProperty BorderRadiusProperty =
            BindableProperty.Create(nameof(BorderRadius), typeof(int), typeof(PXC_Picker), 0);

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(PXC_Picker), Color.Transparent);

        public static readonly BindableProperty FillBackGroungColorProperty =
            BindableProperty.Create(nameof(FillBackGroungColor), typeof(Color), typeof(PXC_Picker), Color.Transparent);

        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create(nameof(HasBorder), typeof(bool), typeof(PXC_Picker), false);

        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(PXC_Picker), new Thickness(0), defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(PXC_Picker), string.Empty);

        public static readonly BindableProperty SelectedValueProperty =
            BindableProperty.Create(nameof(SelectedValue), typeof(Object), typeof(PXC_Picker),
                null, BindingMode.TwoWay, propertyChanged: OnSelectedValueChanged);

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (Equals(newValue, null) && Equals(oldValue, null))
            {
                return;
            }

            var picker = (PXC_Picker)bindable;
            picker.InstanceOnItemsSourceChanged(oldValue, newValue);
        }

        private static void OnSelectedValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var boundPicker = (PXC_Picker)bindable;
            boundPicker.InternalSelectedValueChanged();
        }

        private void InstanceOnItemsSourceChanged(object oldValue, object newValue)
        {
            _disableNestedCalls = true;
            this.Items.Clear();

            var oldCollectionINotifyCollectionChanged = oldValue as INotifyCollectionChanged;
            if (oldCollectionINotifyCollectionChanged != null)
            {
                oldCollectionINotifyCollectionChanged.CollectionChanged -= ItemsSource_CollectionChanged;
            }

            var newCollectionINotifyCollectionChanged = newValue as INotifyCollectionChanged;
            if (newCollectionINotifyCollectionChanged != null)
            {
                newCollectionINotifyCollectionChanged.CollectionChanged += ItemsSource_CollectionChanged;
            }

            if (!Equals(newValue, null))
            {
                var hasDisplayMemberPath = !String.IsNullOrWhiteSpace(this.DisplayMemberPath);

                foreach (var item in (IEnumerable)newValue)
                {
                    if (hasDisplayMemberPath)
                    {
                        var type = item.GetType();
                        var prop = type.GetRuntimeProperty(this.DisplayMemberPath);
                        this.Items.Add(prop.GetValue(item).ToString());
                    }
                    else
                    {
                        this.Items.Add(item.ToString());
                    }
                }

                this.SelectedIndex = -1;
                this._disableNestedCalls = false;

                if (this.SelectedItem != null)
                {
                    this.InternalSelectedItemChanged();
                }
                else if (hasDisplayMemberPath && this.SelectedValue != null)
                {
                    this.InternalSelectedValueChanged();
                }
            }
            else
            {
                _disableNestedCalls = true;
                this.SelectedIndex = -1;
                this.SelectedItem = null;
                this.SelectedValue = null;
                _disableNestedCalls = false;
            }
        }

        private void ItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var hasDisplayMemberPath = !String.IsNullOrWhiteSpace(this.DisplayMemberPath);
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    if (hasDisplayMemberPath)
                    {
                        var type = item.GetType();
                        var prop = type.GetRuntimeProperty(this.DisplayMemberPath);
                        this.Items.Add(prop.GetValue(item).ToString());
                    }
                    else
                    {
                        this.Items.Add(item.ToString());
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.NewItems)
                {
                    if (hasDisplayMemberPath)
                    {
                        var type = item.GetType();
                        var prop = type.GetRuntimeProperty(this.DisplayMemberPath);
                        this.Items.Remove(prop.GetValue(item).ToString());
                    }
                    else
                    {
                        this.Items.Remove(item.ToString());
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (var item in e.NewItems)
                {
                    if (hasDisplayMemberPath)
                    {
                        var type = item.GetType();
                        var prop = type.GetRuntimeProperty(this.DisplayMemberPath);
                        this.Items.Remove(prop.GetValue(item).ToString());
                    }
                    else
                    {
                        var index = this.Items.IndexOf(item.ToString());
                        if (index > -1)
                        {
                            this.Items[index] = item.ToString();
                        }
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                this.Items.Clear();
                if (e.NewItems != null)
                {
                    foreach (var item in e.NewItems)
                    {
                        if (hasDisplayMemberPath)
                        {
                            var type = item.GetType();
                            var prop = type.GetRuntimeProperty(this.DisplayMemberPath);
                            this.Items.Remove(prop.GetValue(item).ToString());
                        }
                        else
                        {
                            var index = this.Items.IndexOf(item.ToString());
                            if (index > -1)
                            {
                                this.Items[index] = item.ToString();
                            }
                        }
                    }
                }
                else
                {
                    _disableNestedCalls = true;
                    this.SelectedItem = null;
                    this.SelectedIndex = -1;
                    this.SelectedValue = null;
                    _disableNestedCalls = false;
                }
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var boundPicker = (PXC_Picker)bindable;
            boundPicker.ItemSelected?.Invoke(boundPicker, new SelectedItemChangedEventArgs(newValue));
            boundPicker.InternalSelectedItemChanged();
        }

        public IEnumerable  ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public Object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set
            {
                if (this.SelectedItem != value)
                {
                    SetValue(SelectedItemProperty, value);
                    InternalSelectedItemChanged();
                }
            }
        }

        private void InternalSelectedItemChanged()
        {
            if (_disableNestedCalls)
            {
                return;
            }

            var selectedIndex = -1;
            Object selectedValue = null;
            if (this.ItemsSource != null)
            {
                var index = 0;
                var hasSelectedValuePath = !String.IsNullOrWhiteSpace(this.SelectedValuePath);
                foreach (var item in this.ItemsSource)
                {
                    if (item != null && item.Equals(this.SelectedItem))
                    {
                        selectedIndex = index;
                        if (hasSelectedValuePath)
                        {
                            var type = item.GetType();
                            var prop = type.GetRuntimeProperty(this.SelectedValuePath);
                            selectedValue = prop.GetValue(item);
                        }
                        break;
                    }
                    index++;
                }
            }
            _disableNestedCalls = true;
            this.SelectedValue = selectedValue;
            this.SelectedIndex = selectedIndex;
            _disableNestedCalls = false;
        }

        public Object SelectedValue
        {
            get { return GetValue(SelectedValueProperty); }
            set
            {
                SetValue(SelectedValueProperty, value);
                InternalSelectedValueChanged();
            }
        }

        private void InternalSelectedValueChanged()
        {
            if (_disableNestedCalls)
            {
                return;
            }

            if (String.IsNullOrWhiteSpace(this.SelectedValuePath))
            {
                return;
            }
            var selectedIndex = -1;
            Object selectedItem = null;
            var hasSelectedValuePath = !String.IsNullOrWhiteSpace(this.SelectedValuePath);
            if (this.ItemsSource != null && hasSelectedValuePath)
            {
                var index = 0;
                foreach (var item in this.ItemsSource)
                {
                    if (item != null)
                    {
                        var type = item.GetType();
                        var prop = type.GetRuntimeProperty(this.SelectedValuePath);
                        if (Object.Equals(prop.GetValue(item), this.SelectedValue))
                        {
                            selectedIndex = index;
                            selectedItem = item;
                            break;
                        }
                    }

                    index++;
                }
            }
            _disableNestedCalls = true;
            this.SelectedItem = selectedItem;
            this.SelectedIndex = selectedIndex;
            _disableNestedCalls = false;
        }

        public String SelectedValuePath { get; set; }

        public PXC_Picker()
        {
            this.SelectedIndexChanged += OnSelectedIndexChanged;
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (_disableNestedCalls)
            {
                return;
            }

            if (this.SelectedIndex < 0 || this.ItemsSource == null || !this.ItemsSource.GetEnumerator().MoveNext())
            {
                _disableNestedCalls = true;
                if (this.SelectedIndex != -1)
                {
                    this.SelectedIndex = -1;
                }
                this.SelectedItem = null;
                this.SelectedValue = null;
                _disableNestedCalls = false;
                return;
            }

            _disableNestedCalls = true;

            var index = 0;
            var hasSelectedValuePath = !String.IsNullOrWhiteSpace(this.SelectedValuePath);
            foreach (var item in this.ItemsSource)
            {
                if (index == this.SelectedIndex)
                {
                    this.SelectedItem = item;
                    if (hasSelectedValuePath)
                    {
                        var type = item.GetType();
                        var prop = type.GetRuntimeProperty(this.SelectedValuePath);
                        this.SelectedValue = prop.GetValue(item);
                    }

                    break;
                }
                index++;
            }

            _disableNestedCalls = false;
        }

        public String DisplayMemberPath { get; set; }

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }

        public int BorderWidth
        {
            get { return (int)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        public int BorderRadius
        {
            get { return (int)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public Color FillBackGroungColor
        {
            get { return (Color)GetValue(FillBackGroungColorProperty); }
            set { SetValue(FillBackGroungColorProperty, value); }
        }
    }
}
