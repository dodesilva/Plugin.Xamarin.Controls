using Plugin.Xamarin.Controls.EnumFiles;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_Editor : Editor
    {
        public PXC_Editor()
        {
            base.TextChanged += Editor_Changed;
        }

        public static readonly BindableProperty MaxLengtProperty = BindableProperty.Create(nameof(MaxLengt), typeof(int), typeof(PXC_Editor), 0);

        public int MaxLengt
        {
            get { return (int)GetValue(MaxLengtProperty); }
            set { SetValue(MaxLengtProperty, value); }
        }

        private void Editor_Changed(object sender, TextChangedEventArgs e)
        {
            Editor editor = sender as Editor;

            string val = editor.Text;
            if (MaxLengt > 0)
            {
                if (val.Length > this.MaxLengt)
                {
                    val = val.Remove(val.Length - 1);
                    editor.Text = val;
                }
            }
            this.InvalidateMeasure();
        }

        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(PXC_Editor), 0);

        public static readonly BindableProperty BorderRadiusProperty =
            BindableProperty.Create(nameof(BorderRadius), typeof(int), typeof(PXC_Editor), 0);

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(PXC_Editor), Color.Transparent);

        public static readonly BindableProperty FillBackGroungColorProperty =
            BindableProperty.Create(nameof(FillBackGroungColor), typeof(Color), typeof(PXC_Editor), Color.Transparent);

        public static readonly BindableProperty HasBorderProperty =
           BindableProperty.Create(nameof(HasBorder), typeof(bool), typeof(PXC_Editor), false);

        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(PXC_Editor), new Thickness(0), defaultBindingMode: BindingMode.OneWay);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(PXC_Editor), default(string));

        public static readonly BindableProperty PlaceholderColorProperty
            = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(PXC_Editor), Color.LightGray);

        public static readonly BindableProperty TextAlignmentProperty = BindableProperty.Create(nameof(TextAlignment), typeof(EditorAlign), typeof(PXC_Editor), default(EditorAlign));

        public static readonly BindableProperty FontNameProperty = BindableProperty.Create(nameof(FontName), typeof(string), typeof(PXC_Editor), "");

        public string FontName
        {
            get { return (string)GetValue(FontNameProperty); }
            set { SetValue(FontNameProperty, value); }
        }

        public EditorAlign TextAlignment
        {
            get { return (EditorAlign)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        public Color PlaceholderColor
        {
            get { return (Color)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set
            {
                SetValue(PlaceholderProperty, value);
            }
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
