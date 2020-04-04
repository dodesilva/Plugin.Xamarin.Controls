using Plugin.Xamarin.Controls.EnumFiles;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_Label_UnderLine:Label
    {
        public event EventHandler Clicked;
        /// <summary>
        /// The is underlined property.
        /// </summary>
        public static readonly BindableProperty IsUnderlineProperty =
            BindableProperty.Create(nameof(IsUnderline), typeof(bool), typeof(PXC_Label_UnderLine), false);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(PXC_Label_UnderLine), default(object), BindingMode.OneWay);

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(PXC_Label_UnderLine), null, BindingMode.OneWay);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        /// <summary>
        /// Gets or sets a value indicating whether the text in the label is underlined.
        /// </summary>
        /// <value>A <see cref="bool"/> indicating if the text in the label should be underlined.</value>
        public bool IsUnderline
        {
            get
            {
                return (bool)GetValue(IsUnderlineProperty);
            }
            set
            {
                SetValue(IsUnderlineProperty, value);
            }
        }

        /// <summary>
        /// The font name property.
        /// </summary>
        public static readonly BindableProperty FontNameProperty =
            BindableProperty.Create(nameof(FontName), typeof(FontType), typeof(PXC_Label_UnderLine), FontType.architep);

        /// <summary>
        /// Gets or sets the name of the font file including extension. If no extension given then ttf is assumed.
        /// Fonts need to be included in projects accoring to the documentation.
        /// </summary>
        /// <value>The full name of the font file including extension.</value>
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

        public int MaxLines { get; set; }

        public PXC_Label_UnderLine()
        {
            var tgr = new TapGestureRecognizer();
            tgr.Tapped += OnTapped;
            GestureRecognizers.Add(tgr);
        }

        protected void OnTapped(object s, EventArgs e)
        {
            if (Command != null && Command.CanExecute(null))
            {
                Command.Execute(CommandParameter ?? this);
            }

            if (Clicked != null)
            {
                Clicked?.Invoke(s, e);
            }
        }
    }
}
