using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_SearchBar: SearchBar
    {
        public static readonly BindableProperty BorderWidthProperty =
                  BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(PXC_SearchBar), 0);

        public static readonly BindableProperty BorderRadiusProperty =
            BindableProperty.Create(nameof(BorderRadius), typeof(int), typeof(PXC_SearchBar), 0);

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(PXC_SearchBar), Color.Transparent);

        public static readonly BindableProperty FillBackGroungColorProperty =
            BindableProperty.Create(nameof(FillBackGroungColor), typeof(Color), typeof(PXC_SearchBar), Color.Transparent);


        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create(nameof(HasBorder), typeof(bool), typeof(PXC_SearchBar), false);

        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(PXC_SearchBar), new Thickness(0), defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty PxcSearchCommandProperty =
            BindableProperty.Create(nameof(PxcSearchCommand), typeof(ICommand), typeof(PXC_SearchBar));

        public ICommand PxcSearchCommand
        {
            get { return (ICommand)GetValue(PxcSearchCommandProperty); }
            set { SetValue(PxcSearchCommandProperty, value); }
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

        public PXC_SearchBar()
        {
            this.TextChanged += PXC_SearchBar_TextChanged;
        }

        private void PXC_SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PxcSearchCommand != null && PxcSearchCommand.CanExecute(this.SearchCommandParameter))
            {
                PxcSearchCommand.Execute(this.SearchCommandParameter);
            }
        }
    }
}
