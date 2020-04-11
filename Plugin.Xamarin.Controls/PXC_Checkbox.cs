using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.EventArgsFile;
using Plugin.Xamarin.Controls.Helpers;
using Plugin.Xamarin.Controls.Interfaces;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_Checkbox : SKCanvasView
    {
        public event EventHandler<CheckChangedArgs> OnCheckChanged;
        #region bindable
        public static readonly BindableProperty UnCheckedColorProperty = BindableProperty.Create(nameof(UnCheckedColor), typeof(Color), typeof(PXC_Checkbox), Color.Gray, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty IconSourceProperty = BindableProperty.Create(nameof(IconSource), typeof(string), typeof(PXC_Checkbox), string.Empty, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty CheckedColorProperty = BindableProperty.Create(nameof(CheckedColor), typeof(Color), typeof(PXC_Checkbox), Color.Black, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(PXC_Checkbox), false, BindingMode.TwoWay, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty CheckedCommandProperty = BindableProperty.Create(nameof(CheckedCommand), typeof(ICommand), typeof(PXC_Checkbox), null, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty CheckedCommandParameterProperty = BindableProperty.Create(nameof(CheckedCommandParameter), typeof(object), typeof(PXC_Checkbox), null, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(float), typeof(PXC_Checkbox), (float)0, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty FontIconProperty = BindableProperty.Create(nameof(IconTypeFace), typeof(Fonts), typeof(PXC_Checkbox), Fonts.None, propertyChanged: OnPropertyChanged);

        private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var box = (PXC_Checkbox)bindable;
            box.InvalidateSurface();
        }
        #endregion
        #region property

        public Fonts IconTypeFace
        {
            get { return (Fonts)GetValue(FontIconProperty); }
            set { SetValue(FontIconProperty, value); }
        }
        public string IconSource
        {
            get { return (string)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }
        public object CheckedCommandParameter
        {
            get { return GetValue(CheckedCommandParameterProperty); }
            set { SetValue(CheckedCommandParameterProperty, value); }
        }
        public ICommand CheckedCommand
        {
            get { return (ICommand)GetValue(CheckedCommandProperty); }
            set { SetValue(CheckedCommandProperty, value); }
        }

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
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
        public float BorderWidth
        {
            get { return (float)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }
        #endregion
        private SKColor skBorderColor;
        public PXC_Checkbox()
        {
            HeightRequest = 35;
            WidthRequest = 35;
            EnableTouchEvents = true;
        }
        
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);
            var info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();
            float width = (float)Width * 2;
            skBorderColor = UnCheckedColor.ToSKColor();
            if (IsChecked)
            {
                skBorderColor = CheckedColor.ToSKColor();
            }
            SKPaint Borderpaint = new SKPaint()
            {
                Color = skBorderColor,
                StrokeWidth = BorderWidth,
                StrokeCap = SKStrokeCap.Round,
                Style = SKPaintStyle.Stroke,
                FilterQuality = SKFilterQuality.High,
                IsAntialias = true,
            };

            canvas.DrawRoundRect(SKRect.Create(BorderWidth / 2, BorderWidth / 2, width, width), 15, 15, Borderpaint);
            canvas.Save();
            canvas.Restore();
            if (IsChecked)
            {
                IIcon icon = Helpers.Extensions.FindIconForKey(IconSource, IconTypeFace);

                using (var paint = new SKPaint())
                {
                    paint.TextSize = width;
                    paint.IsAntialias = true;
                    paint.Style = SKPaintStyle.Fill;
                    paint.Color = skBorderColor;
                    paint.IsStroke = false;
                    if (IconTypeFace == Fonts.FontAwesome)
                        paint.Typeface = SKTypeface.FromStream(GetManifestFontStream("Plugin.Xamarin.Controls.Resources.fontawesome.ttf"));
                    if (IconTypeFace == Fonts.Material)
                        paint.Typeface = SKTypeface.FromStream(GetManifestFontStream("Plugin.Xamarin.Controls.Resources.materialicons.ttf"));
                    if (IconTypeFace == Fonts.IconMoon)
                        paint.Typeface = SKTypeface.FromStream(GetManifestFontStream("Plugin.Xamarin.Controls.Resources.icomoon.ttf"));

                    canvas.DrawText(string.Format("{0}", icon.Character), 3, width + 2, paint);
                }
            }
        }
        protected override void OnTouch(SKTouchEventArgs e)
        {
            base.OnTouch(e);
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    InvalidateSurface();
                    if (e.InContact)
                    {
                        IsChecked = !IsChecked;                        
                        OnTappedChanged(IsChecked);

                        e.Handled = true;
                    }
                    break;
            }
        }
        private void OnTappedChanged(bool isChecked)
        {
            if (OnCheckChanged != null)
                OnCheckChanged?.Invoke(this, new CheckChangedArgs(isChecked,CheckedCommandParameter));
            if (CheckedCommand != null && CheckedCommand.CanExecute(CheckedCommandParameter))
                CheckedCommand.Execute(CheckedCommandParameter);
        }
        public Stream GetManifestFontStream(string resourceName)
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            return assembly.GetManifestResourceStream(resourceName);
        }
    }
}
