using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_StackLayout: StackLayout
    {
        public event EventHandler Clicked;
        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(PXC_StackLayout), 0);

        public static readonly BindableProperty BorderRadiusProperty =
            BindableProperty.Create(nameof(BorderRadius), typeof(int), typeof(PXC_StackLayout), 0);

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(PXC_StackLayout), Color.Transparent);

        public static readonly BindableProperty FillBackGroungColorProperty =
            BindableProperty.Create(nameof(FillBackGroungColor), typeof(Color), typeof(PXC_StackLayout), Color.Transparent);

        public static readonly BindableProperty CommandParameterProperty = 
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(PXC_StackLayout), default(object), BindingMode.OneWay);

        public object CommandParameter
        {
            get{return (object)GetValue(CommandParameterProperty);}
            set{ SetValue(CommandParameterProperty, value);}
        }

        public static readonly BindableProperty CommandProperty = 
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(PXC_StackLayout), null, BindingMode.OneWay);

        public ICommand Command
        {
            get{ return (ICommand)GetValue(CommandProperty); }
            set{ SetValue(CommandProperty, value);}
        }

        public PXC_StackLayout()
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
