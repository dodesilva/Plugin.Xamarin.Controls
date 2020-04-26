using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.Interfaces;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_Button : SKCanvasView
    {
<<<<<<< HEAD
        #region Bindable
        public static BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create(nameof(SelectedBackgroundColor), typeof(Color),
            typeof(PXC_Button), Color.Transparent, BindingMode.OneWay,
            validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);
=======
>>>>>>> 712d3873961ff4c4091c5fe5a63e82a82f9ae8e8

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(float), typeof(PXC_Button), (float)0f, BindingMode.OneWay,
            validateValue: (_, value) => value != null && (float)value >= 0,
            propertyChanged: OnPropertyChangedInvalidate);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(PXC_Button), Color.Transparent, BindingMode.OneWay,
           validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);
        public static readonly BindableProperty IconTypeFaceProperty =
        BindableProperty.Create(nameof(IconTypeFace), typeof(Fonts), typeof(PXC_Button), Fonts.None, BindingMode.OneWay, propertyChanged: OnPropertyChangedInvalidate);

        public static readonly BindableProperty IconSourceProperty =
       BindableProperty.Create(nameof(IconSource), typeof(string), typeof(PXC_Button), string.Empty, BindingMode.OneWay, propertyChanged: OnPropertyChangedInvalidate);

        public static readonly BindableProperty SelectedIconSourceProperty =
       BindableProperty.Create(nameof(SelectedIconSource), typeof(string), typeof(PXC_Button), string.Empty, BindingMode.OneWay, propertyChanged: OnPropertyChangedInvalidate);

        public static readonly BindableProperty TextFloatProperty =
            BindableProperty.Create(nameof(TextFloat), typeof(TextFloting), typeof(PXC_Button), default(TextFloting), BindingMode.OneWay, propertyChanged: OnPropertyChangedInvalidate);

        public static BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float),
            typeof(PXC_Button), 5f, BindingMode.OneWay,
            validateValue: (_, value) => value != null && (float)value >= 0,
            propertyChanged: OnPropertyChangedInvalidate);

        public static BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(float),
            typeof(PXC_Button), 12f, BindingMode.OneWay, validateValue: (_, value) => value != null && (float)value >= 0,
            propertyChanged: OnPropertyChangedInvalidate);

        public static BindableProperty StartColorProperty = BindableProperty.Create(nameof(StartColor), typeof(Color),
           typeof(PXC_Button), Color.Transparent, BindingMode.OneWay,
           validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public static BindableProperty EndColorProperty = BindableProperty.Create(nameof(EndColor), typeof(Color),
            typeof(PXC_Button), Color.Transparent, BindingMode.OneWay,
            validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public static BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color),
            typeof(PXC_Button), Color.Black, BindingMode.OneWay,
            validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public static BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand),
            typeof(PXC_Button), null, BindingMode.OneWay);

        public static BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object),
            typeof(PXC_Button), null, BindingMode.OneWay);

        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string),
            typeof(PXC_Button), string.Empty, BindingMode.OneWay, propertyChanged: OnPropertyChangedInvalidate);

        public static BindableProperty HasSelectedProperty = BindableProperty.Create(nameof(HasSelected), typeof(bool),
            typeof(PXC_Button), false, BindingMode.TwoWay, validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public static BindableProperty UseEnabledProperty = BindableProperty.Create(nameof(UseEnabled), typeof(bool),
            typeof(PXC_Button), false, BindingMode.TwoWay, validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public static BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool),
            typeof(PXC_Button), false, BindingMode.TwoWay, validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public static BindableProperty SelectedTextProperty = BindableProperty.Create(nameof(SelectedText), typeof(string),
            typeof(PXC_Button), string.Empty, BindingMode.OneWay, propertyChanged: OnPropertyChangedInvalidate);

        public static BindableProperty SelectedTextColorProperty = BindableProperty.Create(nameof(SelectedTextColor), typeof(Color),
            typeof(PXC_Button), Color.Gray, BindingMode.OneWay,
            validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        private static void OnPropertyChangedInvalidate(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (PXC_Button)bindable;

            if (oldvalue != newvalue)
                control.InvalidateSurface();
        }
        #endregion
        #region Property
        public bool UseEnabled
        {
            get { return (bool)GetValue(UseEnabledProperty); }
            set { SetValue(UseEnabledProperty, value); }
        }
        public Color SelectedBackgroundColor
        {
            get => (Color)GetValue(SelectedBackgroundColorProperty);
            set => SetValue(SelectedBackgroundColorProperty, value);
        }
        public Color SelectedTextColor
        {
            get => (Color)GetValue(SelectedTextColorProperty);
            set => SetValue(SelectedTextColorProperty, value);
        }
        public string SelectedIconSource
        {
            get => (string)GetValue(SelectedIconSourceProperty);
            set => SetValue(SelectedIconSourceProperty, value);
        }
        public string SelectedText
        {
            get => (string)GetValue(SelectedTextProperty);
            set => SetValue(SelectedTextProperty, value);
        }
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
        public bool HasSelected
        {
            get => (bool)GetValue(HasSelectedProperty);
            set => SetValue(HasSelectedProperty, value);
        }
        public float BorderWidth
        {
            get { return (float)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
        public TextFloting TextFloat
        {
            get { return (TextFloting)GetValue(TextFloatProperty); }
            set { SetValue(TextFloatProperty, value); }
        }
        public Fonts IconTypeFace
        {
            get { return (Fonts)GetValue(IconTypeFaceProperty); }
            set { SetValue(IconTypeFaceProperty, value); }
        }
        public string IconSource
        {
            get { return (string)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }
        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        public float FontSize
        {
            get => (float)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }
        public Color StartColor
        {
            get => (Color)GetValue(StartColorProperty);
            set => SetValue(StartColorProperty, value);
        }
        public Color EndColor
        {
            get => (Color)GetValue(EndColorProperty);
            set => SetValue(EndColorProperty, value);
        }
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        #endregion

        public PXC_Button()
        {
            EnableTouchEvents = true;
        }

        private SKColor skStartColor;
        private SKColor skEndColor;
        private SKColor skborderColor;
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var canvas = e.Surface.Canvas;
            float width = (float)Width;
            var scale = CanvasSize.Width / width;
            var cornerRadius = CornerRadius * scale;
            var textSize = FontSize * scale;
            var height = e.Info.Height;
            var str = string.Empty;
            var iconstr = string.Empty;
            float direction = 0;

            canvas.Clear();

            var controlRect = new SKRect(BorderWidth, BorderWidth, info.Width - BorderWidth, height - BorderWidth);
            var backgroundBar = new SKRoundRect(controlRect, cornerRadius, cornerRadius);
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;
                paint.Color = Color.White.ToSKColor();
                paint.Style = SKPaintStyle.Fill;
                skStartColor = StartColor.ToSKColor();
                skEndColor = EndColor.ToSKColor();
                skborderColor = BorderColor.ToSKColor();
                str = Text;
                if (HasSelected)
                {
                    if (IsSelected)
                    {
                        paint.Color = SelectedBackgroundColor.ToSKColor();
                        skStartColor = Color.Default.ToSKColor();
                        skEndColor = Color.Default.ToSKColor();
                        skborderColor = SelectedTextColor.ToSKColor();
                        str = SelectedText;
                        if (UseEnabled)
                            IsEnabled = false;
                    }
                }

                float y = info.Height;
                if (skStartColor != Color.Default.ToSKColor() || skEndColor != Color.Default.ToSKColor())
                {
                    paint.Shader = SKShader.CreateLinearGradient(
                        new SKPoint(controlRect.Left, controlRect.Top),
                        new SKPoint(controlRect.Right, controlRect.Top),
                        new[]
                        {
                           skStartColor,
                           skEndColor
                        },
                        new float[] { 0, 1 },
                        SKShaderTileMode.Clamp);
                }
                canvas.DrawRoundRect(backgroundBar, paint);
                canvas.Save();
                canvas.Restore();
                if (BorderColor != Color.Default)
                {
                    SKPaint Borderpaint = new SKPaint()
                    {
                        Color = skborderColor,
                        StrokeWidth = BorderWidth,
                        StrokeCap = SKStrokeCap.Round,
                        Style = SKPaintStyle.Stroke,
                        FilterQuality = SKFilterQuality.High,
                        IsAntialias = true,
                    };
                    canvas.DrawRoundRect(backgroundBar, Borderpaint);
                }
            }
           
            var textBounds = new SKRect();
            var textPaint = new SKPaint
            {
                TextSize = textSize
            };
            var painticon = new SKPaint();
            textPaint.MeasureText(str, ref textBounds);
            var xText = CanvasSize.Width / 2 - textBounds.MidX;
            var yText = info.Height / 2 - textBounds.MidY;
            var widthRect = textBounds.Width / 3 + 40;

            if (!string.IsNullOrEmpty(IconSource))
            {
                var txtsize = FontSize * 4;
                painticon.TextSize = txtsize;
                if (TextFloat == TextFloting.Right)
                {
                    xText = xText + 30;
                    direction = xText - 80;
                }
                else if (TextFloat == TextFloting.Left)
                {
                    xText = xText - 30;
                    direction = textBounds.Right + xText+10;
                }
                widthRect += FontSize;
            }
            textPaint.Color = TextColor.ToSKColor();
            painticon.Color = TextColor.ToSKColor();

            iconstr = IconSource;

            if (HasSelected)
            {
                if (IsSelected)
                {
                    textPaint.Color = SelectedTextColor.ToSKColor();
                    painticon.Color = SelectedTextColor.ToSKColor();
                    iconstr = SelectedIconSource;
                }
            }
            IIcon icon = Helpers.Extensions.FindIconForKey(iconstr, IconTypeFace);
            if (!string.IsNullOrEmpty(Text))
            {
                canvas.DrawText(str, xText, yText, textPaint);
                WidthRequest = widthRect;
            }

            if (icon != null)
            {
                painticon.IsAntialias = true;
                painticon.Style = SKPaintStyle.Fill;
                painticon.IsStroke = false;
                if (IconTypeFace == Fonts.FontAwesome)
                    painticon.Typeface = SKTypeface.FromStream(GetManifestFontStream("Plugin.Xamarin.Controls.Resources.fontawesome.ttf"));
                if (IconTypeFace == Fonts.Material)
                    painticon.Typeface = SKTypeface.FromStream(GetManifestFontStream("Plugin.Xamarin.Controls.Resources.materialicons.ttf"));
                if (IconTypeFace == Fonts.IconMoon)
                    painticon.Typeface = SKTypeface.FromStream(GetManifestFontStream("Plugin.Xamarin.Controls.Resources.icomoon.ttf"));


                if (string.IsNullOrEmpty(Text))
                {
                    painticon.MeasureText(icon.Character.ToString(), ref textBounds);
                    var x = CanvasSize.Width / 2 - textBounds.MidX;
                    var y = info.Height / 2 - textBounds.MidY;
                    canvas.DrawText(string.Format("{0}", icon.Character), x, y, painticon);
                }
                else
                {
                    canvas.DrawText(string.Format("{0}", icon.Character), direction, yText + 15, painticon);
                }
            }
        }

<<<<<<< HEAD
        protected override void OnTouch(SKTouchEventArgs e)
        {
            switch (e.ActionType)
            {
                case SKTouchAction.Released:
                    InvalidateSurface();
                    IsSelected = !IsSelected;
                    if (Command != null && Command.CanExecute(CommandParameter))
                    {
                        Command.Execute(CommandParameter);
                    }

                    RaiseClickedEvent();
                    break;
            }

            e.Handled = true;
        }

        protected void RaiseClickedEvent()
        {
            Clicked?.Invoke(this, new TappedEventArgs(CommandParameter));
        }

        public event EventHandler<TappedEventArgs> Clicked;

        public Stream GetManifestFontStream(string resourceName)
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            return assembly.GetManifestResourceStream(resourceName);
        }
=======
>>>>>>> 712d3873961ff4c4091c5fe5a63e82a82f9ae8e8
    }
}