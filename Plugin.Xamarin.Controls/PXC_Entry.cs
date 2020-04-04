using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_Entry:Entry
    {
        public static readonly BindableProperty BorderWidthProperty =
               BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(PXC_Entry), 0);

        public static readonly BindableProperty BorderRadiusProperty =
            BindableProperty.Create(nameof(BorderRadius), typeof(int), typeof(PXC_Entry), 0);

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(PXC_Entry), Color.Transparent);

        public static readonly BindableProperty FillBackGroungColorProperty =
            BindableProperty.Create(nameof(FillBackGroungColor), typeof(Color), typeof(PXC_Entry), Color.Transparent);


        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create(nameof(HasBorder), typeof(bool), typeof(PXC_Entry), false);

        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(PXC_Entry), new Thickness(0), defaultBindingMode: BindingMode.OneWay);

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

