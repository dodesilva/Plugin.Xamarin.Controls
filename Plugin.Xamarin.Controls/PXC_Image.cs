using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_Image: Image
    {
        public event EventHandler Clicked;

        public static readonly BindableProperty BorderWidthProperty =
          BindableProperty.Create(propertyName: nameof(BorderWidth),
              returnType: typeof(int),
              declaringType: typeof(PXC_Image),
              defaultValue: 0);

       
        public int BorderWidth
        {
            get { return (int)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

       
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(propertyName: nameof(BorderColor),
              returnType: typeof(Color),
              declaringType: typeof(PXC_Image),
              defaultValue: Color.White);


       
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        
        public static readonly BindableProperty FillBackGroungColorProperty =
            BindableProperty.Create(propertyName: nameof(FillBackGroungColor),
              returnType: typeof(Color),
              declaringType: typeof(PXC_Image),
              defaultValue: Color.Transparent);

       
        public Color FillBackGroungColor
        {
            get { return (Color)GetValue(FillBackGroungColorProperty); }
            set { SetValue(FillBackGroungColorProperty, value); }
        }

        public static readonly BindableProperty IsCircleProperty =
           BindableProperty.Create(nameof(IsCircle), typeof(bool), typeof(PXC_Image), false);

        public bool IsCircle
        {
            get { return (bool)GetValue(IsCircleProperty); }
            set { SetValue(IsCircleProperty, value); }
        }

        public static readonly BindableProperty BorderRadiusProperty =
            BindableProperty.Create(nameof(BorderRadius), typeof(int), typeof(PXC_Image), 0);

        public int BorderRadius
        {
            get { return (int)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(PXC_Image), default(object), BindingMode.OneWay);

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(PXC_Image), null, BindingMode.OneWay);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public PXC_Image()
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

            if (Clicked!=null)
            {
                Clicked?.Invoke(s, e);
            }
        }
    }
}
