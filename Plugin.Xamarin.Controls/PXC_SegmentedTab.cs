using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_SegmentedTab: ContentView
    {
        StackLayout _mainContentLayout = new StackLayout() { Spacing = 10, Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };
        StackLayout _lastElementSelected;
        ScrollView _mainLayout = new ScrollView() { VerticalOptions = LayoutOptions.Start, Orientation = ScrollOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };
        
        public static readonly BindableProperty ItemSelectedProperty = BindableProperty.Create(nameof(ItemSelected), typeof(string), typeof(PXC_SegmentedTab), null);
        public static readonly BindableProperty FontIconNameProperty = BindableProperty.Create(nameof(FontIconName), typeof(Fonts), typeof(PXC_SegmentedTab), Fonts.None);
        public static readonly BindableProperty ChildrenProperty = BindableProperty.Create(nameof(Children), typeof(List<BarIconAndTitle>), typeof(PXC_SegmentedTab), null);
        public static readonly BindableProperty UnSelectedColorProperty = BindableProperty.Create(nameof(UnSelectedColor), typeof(Color), typeof(PXC_SegmentedTab), Color.DarkGray);
        public static readonly BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create(nameof(SelectedBackgroundColor), typeof(Color), typeof(PXC_SegmentedTab), Color.Transparent);
        public static readonly BindableProperty UnSelectedBackgroundColorProperty = BindableProperty.Create(nameof(UnSelectedBackgroundColor), typeof(Color), typeof(PXC_SegmentedTab), Color.Transparent);
        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(PXC_SegmentedTab), Color.Black);
       public static readonly BindableProperty AutoScrollProperty = BindableProperty.Create(nameof(AutoScroll), typeof(bool), typeof(PXC_SegmentedTab), true);
        public static readonly BindableProperty SelectedItemChangedCommandProperty = BindableProperty.Create(nameof(SelectedItemChangedCommand), typeof(Command<string>), typeof(PXC_SegmentedTab), default(Command<string>), BindingMode.TwoWay, null, SelectedItemChangedCommandPropertyChanged);
        static void SelectedItemChangedCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var source = bindable as PXC_SegmentedTab;
            if (source == null)
            {
                return;
            }
            source.SelectedItemChangedCommandChanged();
        }
        private void SelectedItemChangedCommandChanged()
        {
            OnPropertyChanged("SelectedItemChangedCommand");
        }
        public Command<string> SelectedItemChangedCommand
        {
            get
            {
                return (Command<string>)GetValue(SelectedItemChangedCommandProperty);
            }
            set
            {
                SetValue(SelectedItemChangedCommandProperty, value);
            }
        }
        public delegate void SelectedItemChangedEventHandler(object sender, SelectedItemChangedEventArgs e);
        public event SelectedItemChangedEventHandler SelectedItemChanged;
        public string ItemSelected
        {
            get
            {
                return (string)GetValue(ItemSelectedProperty);
            }
            set
            {
                SetValue(ItemSelectedProperty, value);
                SelectedItemChanged(this, new SelectedItemChangedEventArgs(value));
                SelectedItemChangedCommand?.Execute(value);
            }
        }
        public List<BarIconAndTitle> Children
        {
            get
            {
                return (List<BarIconAndTitle>)GetValue(ChildrenProperty);
            }
            set
            {
                SetValue(ChildrenProperty, value);
            }
        }
        public Fonts FontIconName
        {
            get
            {
                return (Fonts)GetValue(FontIconNameProperty);
            }
            set
            {
                SetValue(FontIconNameProperty, value);
            }
        }
        public Color UnSelectedColor
        {
            get{ return (Color)GetValue(UnSelectedColorProperty);}
            set{ SetValue(UnSelectedColorProperty, value);}
        }
        public Color SelectedColor
        {
            get{return (Color)GetValue(SelectedColorProperty);}
            set{ SetValue(SelectedColorProperty, value);}
        }
        
        public Color SelectedBackgroundColor
        {
            get{ return (Color)GetValue(SelectedBackgroundColorProperty);}
            set{SetValue(SelectedBackgroundColorProperty, value); }
        }
        public Color UnSelectedBackgroundColor
        {
            get{ return (Color)GetValue(UnSelectedBackgroundColorProperty);}
            set{ SetValue(UnSelectedBackgroundColorProperty, value);}
        }
        public bool AutoScroll
        {
            get{return (bool)GetValue(AutoScrollProperty);}
            set{ SetValue(AutoScrollProperty, value);}
        }
        void LoadChildrens()
        {
            _mainContentLayout.Children.Clear();
            foreach (var item in Children)
            {
                var boxview = new BoxView() { BackgroundColor = Color.Transparent, HeightRequest = 2, HorizontalOptions = LayoutOptions.FillAndExpand };
                var childrenLayout = new StackLayout() { Spacing = 5 , HorizontalOptions = LayoutOptions.FillAndExpand };
                var childrenLayoutLabel = new StackLayout() { HorizontalOptions=LayoutOptions.CenterAndExpand,Orientation=StackOrientation.Horizontal };
                if (FontIconName != Fonts.None)
                {
                    if (!string.IsNullOrEmpty(item.IconText))
                    {
                        var labelicon = new PXC_Label()
                        {
                            FontAttributes = FontAttributes.Bold,
                            Icon = item.IconText,
                            TextColor = UnSelectedColor,
                            FontSize = 30,
                            FontIconName = FontIconName,
                            Margin = new Thickness(0, 5),
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Start
                        };
                        childrenLayoutLabel.Children.Add(labelicon);
                    }
                }
                if (!string.IsNullOrEmpty(item.Title))
                {
                    var label = new Label()
                    {
                        FontAttributes = FontAttributes.Bold,
                        Text = item.Title,
                        TextColor = UnSelectedColor,
                        Margin = new Thickness(5, 0),
                        VerticalOptions=LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.End
                    };
                    childrenLayoutLabel.Children.Add(label);
                }
                
                childrenLayout.Children.Add(childrenLayoutLabel);
                childrenLayout.Children.Add(boxview);
                childrenLayout.ClassId = item.Title;
                _mainContentLayout.Children.Add(childrenLayout);
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) => {
                    ItemSelected = ((StackLayout)s).ClassId;
                    SelectElement((StackLayout)s);
                };
                childrenLayout.GestureRecognizers.Add(tapGestureRecognizer);

                if (ItemSelected==null)
                { 
                    SelectElement((StackLayout)childrenLayout);
                    var itm = Children.First().Title;
                    ItemSelected = itm;
                   
                }
            }
            _mainLayout.Content = _mainContentLayout;
            var mainContentLayout = new StackLayout() { Spacing = 0, HorizontalOptions = LayoutOptions.FillAndExpand };
            mainContentLayout.Children.Add(_mainLayout);
            mainContentLayout.Children.Add(new BoxView() { HeightRequest = 0.5, HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.Silver });
            this.Content = mainContentLayout;
        }
        void SelectElement(StackLayout SelectedLayout)
        {
            if (_lastElementSelected != null)
            {
                if (UnSelectedBackgroundColor != Color.Transparent)
                {
                    _lastElementSelected.BackgroundColor = UnSelectedBackgroundColor;
                }
                (_lastElementSelected.Children.First(p => p is BoxView) as BoxView).BackgroundColor = Color.Transparent;
                ((_lastElementSelected.Children.First(p => p is StackLayout) as StackLayout).Children.First(m => m is PXC_Label) as PXC_Label).TextColor = UnSelectedColor;
                ((_lastElementSelected.Children.First(p => p is StackLayout) as StackLayout).Children.Last(m => m is Label) as Label).TextColor = UnSelectedColor;
            }
            if (SelectedBackgroundColor != Color.Transparent)
            {
                SelectedLayout.BackgroundColor = SelectedBackgroundColor;
            }
            (SelectedLayout.Children.First(p => p is BoxView) as BoxView).BackgroundColor = SelectedColor;
            ((SelectedLayout.Children.First(p => p is StackLayout) as StackLayout).Children.First(m => m is PXC_Label) as PXC_Label).TextColor = SelectedColor;
            ((SelectedLayout.Children.First(p => p is StackLayout) as StackLayout).Children.Last(m => m is Label) as Label).TextColor = SelectedColor;
            _lastElementSelected = SelectedLayout;
            if (AutoScroll)
            {
                if (ItemSelected != null)
                {
                    _mainLayout.ScrollToAsync(SelectedLayout, ScrollToPosition.MakeVisible, true);
                }
            }
        }
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

             if (propertyName == ChildrenProperty.PropertyName && Children != null)
            {
                LoadChildrens();              
            }
        }
    }
}
