using Plugin.Xamarin.Controls.EnumFiles;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_Button:Button
    {
        public static readonly BindableProperty ItemPaddingProperty =
            BindableProperty.Create(nameof(ItemPadding), typeof(Thickness), typeof(PXC_Button), new Thickness(0), defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty FontIconProperty =
        BindableProperty.Create(nameof(FontIconName), typeof(Fonts), typeof(PXC_Button), Fonts.None);

        public static readonly BindableProperty IconProperty =
       BindableProperty.Create(nameof(Icon), typeof(string), typeof(PXC_Button), string.Empty);

        public static readonly BindableProperty TextFloatProperty = 
            BindableProperty.Create(nameof(TextFloat), typeof(TextFloting), typeof(PXC_Button), default(TextFloting));

        public TextFloting TextFloat
        {
            get { return (TextFloting)GetValue(TextFloatProperty); }
            set { SetValue(TextFloatProperty, value); }
        }

        public Fonts FontIconName
        {
            get { return (Fonts)GetValue(FontIconProperty); }
            set { SetValue(FontIconProperty, value); }
        }

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public Thickness ItemPadding
        {
            get { return (Thickness)GetValue(ItemPaddingProperty); }
            set { SetValue(ItemPaddingProperty, value); }
        }
        
    }
}
