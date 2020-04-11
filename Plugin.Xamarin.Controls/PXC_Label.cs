using Plugin.Xamarin.Controls.EnumFiles;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_Label:Label
    {
        public event EventHandler Clicked;

        public static readonly BindableProperty FontIconProperty =
        BindableProperty.Create(nameof(FontIconName), typeof(Fonts), typeof(PXC_Label), Fonts.None);

        public static readonly BindableProperty IconProperty =
       BindableProperty.Create(nameof(Icon), typeof(string), typeof(PXC_Label), string.Empty);

        public static readonly BindableProperty TextFloatProperty =
            BindableProperty.Create(nameof(TextFloat), typeof(TextFloting), typeof(PXC_Label), default(TextFloting));

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

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(PXC_Label), default(object), BindingMode.OneWay);

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(PXC_Label), null, BindingMode.OneWay);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
       
        /// <summary>
        /// The font name property.
        /// </summary>
        public static readonly BindableProperty FontNameProperty =
            BindableProperty.Create(nameof(FontName), typeof(FontType), typeof(PXC_Label), FontType.None);

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

        public PXC_Label()
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
