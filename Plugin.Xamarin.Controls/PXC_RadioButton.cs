using Plugin.Xamarin.Controls.EnumFiles;
using System;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_RadioButton:View
    {
        /// <summary>
        /// The checked property
        /// </summary>
        public static readonly BindableProperty CheckedProperty = BindableProperty.Create(nameof(Checked), typeof(bool), typeof(PXC_RadioButton));

        /// <summary>
        ///     The default text property.
        /// </summary>
        public static readonly BindableProperty TextProperty =BindableProperty.Create(nameof(Text),typeof(string),typeof(PXC_RadioButton),string.Empty);

        /// <summary>
        ///     The default text property.
        /// </summary>
        public static readonly BindableProperty TextColorProperty =BindableProperty.Create(nameof(TextColor),typeof(Color),typeof(PXC_RadioButton),Color.Default);

        /// <summary>
        ///     The default color checked property.
        /// </summary>
        public static readonly BindableProperty CheckedColorProperty = BindableProperty.Create(nameof(CheckedColor), typeof(Color), typeof(PXC_RadioButton), Color.Default);

        /// <summary>
        ///     The default color unchecked property.
        /// </summary>
        public static readonly BindableProperty UnCheckedColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(PXC_RadioButton), Color.Default);

        /// <summary>
        /// The font size property
        /// </summary>
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize),typeof(double),typeof(PXC_RadioButton));

        /// <summary>
        /// The font name property.
        /// </summary>
        public static readonly BindableProperty FontNameProperty =
            BindableProperty.Create(nameof(FontName),typeof(FontType),typeof(PXC_RadioButton), FontType.OpenSans_Regular);



        /// <summary>
        ///     The checked changed event.
        /// </summary>
        public EventHandler<bool> CheckedChanged;

        /// <summary>
        ///     Gets or sets a value indicating whether the control is checked.
        /// </summary>
        /// <value>The checked state.</value>
        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }

            set
            {
                SetValue(CheckedProperty, value);
                var eventHandler = CheckedChanged;

                if (eventHandler != null)
                {
                    eventHandler.Invoke(this, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }

            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }

            set { SetValue(TextColorProperty, value); }
        }

        public Color CheckedColor
        {
            get { return (Color)GetValue(CheckedColorProperty); }

            set { SetValue(CheckedColorProperty, value); }
        }

        public Color UnCheckedColor
        {
            get { return (Color)GetValue(UnCheckedColorProperty); }

            set { SetValue(UnCheckedColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        /// <value>The size of the font.</value>
        public double FontSize
        {
            get
            {
                return (double)GetValue(FontSizeProperty);
            }
            set
            {
                SetValue(FontSizeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the font.
        /// </summary>
        /// <value>The name of the font.</value>
        public FontType FontName
        {
            get
            {
                return (FontType)GetValue(FontNameProperty);
            }
            set
            {
                SetValue(FontNameProperty, value);
            }
        }

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    }
}
