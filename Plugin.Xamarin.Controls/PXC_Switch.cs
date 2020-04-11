using Plugin.Xamarin.Controls.EventArgsFile;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_Switch: SKCanvasView
    {
        public event EventHandler<CheckChangedArgs> ToggledChanged;
        #region Bindables
        public static readonly BindableProperty UnToggledColorProperty = BindableProperty.Create(nameof(UnToggledColor), typeof(Color), typeof(PXC_Switch), Color.FromHex("#ccc"));
        public static readonly BindableProperty ToggledColorProperty = BindableProperty.Create(nameof(ToggledColor), typeof(Color), typeof(PXC_Switch), Color.LightBlue);
        public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(PXC_Switch), false, BindingMode.TwoWay);
        public static readonly BindableProperty ToggledCommandProperty = BindableProperty.Create(nameof(ToggledCommand), typeof(ICommand), typeof(PXC_Switch), null);
        public static readonly BindableProperty ToggledCommandParameterProperty = BindableProperty.Create(nameof(ToggledCommandParameter), typeof(object), typeof(PXC_Switch), null);
        public static readonly BindableProperty ThumbColorProperty = BindableProperty.Create(nameof(ThumbColor), typeof(Color), typeof(PXC_Switch), Color.White);
       
        #endregion
        #region Property
        public object ToggledCommandParameter
        {
            get { return GetValue(ToggledCommandParameterProperty); }
            set { SetValue(ToggledCommandParameterProperty, value); }
        }
        public ICommand ToggledCommand
        {
            get { return (ICommand)GetValue(ToggledCommandProperty); }
            set { SetValue(ToggledCommandProperty, value); }
        }
        public bool IsToggled
        {
            get { return (bool)GetValue(IsToggledProperty); }
            set { SetValue(IsToggledProperty, value); }
        }        
        public Color ToggledColor
        {
            get { return (Color)GetValue(ToggledColorProperty); }
            set
            {
                SetValue(ToggledColorProperty, value);
            }

        }
        public Color UnToggledColor
        {
            get { return (Color)GetValue(UnToggledColorProperty); }
            set
            {
                SetValue(UnToggledColorProperty, value);
            }
        }
        public Color ThumbColor
        {
            get { return (Color)GetValue(ThumbColorProperty); }
            set
            {
                SetValue(ThumbColorProperty, value);
                InvalidateSurface();
            }
        }
       
        #endregion
        public PXC_Switch() : base()
        {
            EnableTouchEvents = true;
            CXtoCircle = 70;
            CYtoCircle = 70;
            WidthRequest = 45;
            HeightRequest = 25;
        }

        private void OnToggledChanged(bool isToggled)
        {
            if (ToggledChanged != null)
                ToggledChanged?.Invoke(this, new CheckChangedArgs(isToggled,ToggledCommandParameter));
            if (ToggledCommand != null && ToggledCommand.CanExecute(ToggledCommandParameter))
                ToggledCommand.Execute(ToggledCommandParameter);
        }
        
        private float CXtoCircle = 0;
        private float CYtoCircle = 0;
        private SKColor CheckUncheckColor;
        SKCanvas canvas;

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            canvas = surface.Canvas;
            float width = (float)Width;
            var scale = CanvasSize.Width / width;
            var cornerRadius = 24 * scale;
            CXtoCircle = 75;
            CYtoCircle = 75;
                      
            CheckUncheckColor = UnToggledColor.ToSKColor();
            if (IsToggled)
            {
                CXtoCircle = info.Width * 2 - CYtoCircle;
                CheckUncheckColor = ToggledColor.ToSKColor();
            }
            
            SKPaint Filepaint = new SKPaint()
            {
                Color = CheckUncheckColor,
                StrokeCap = SKStrokeCap.Round,
                Style = SKPaintStyle.StrokeAndFill,
                FilterQuality = SKFilterQuality.High,
                IsAntialias = true,
            };
            SKPaint Borderpaint = new SKPaint()
            {
                Color = CheckUncheckColor,
                StrokeWidth = 10,
                StrokeCap = SKStrokeCap.Round,
                Style = SKPaintStyle.Stroke,
                FilterQuality = SKFilterQuality.High,
                IsAntialias = true,
            };
            SKPaint Thumbpaint = new SKPaint()
            {
                Color = ThumbColor.ToSKColor(),
                Style = SKPaintStyle.StrokeAndFill,
                FilterQuality = SKFilterQuality.High,
                IsAntialias = true
            };

            canvas.Clear();

            canvas.DrawRoundRect(SKRect.Create(0, 0, info.Width, info.Height), 100, 100, Filepaint);
            canvas.Save();
            canvas.Restore();
            canvas.DrawRoundRect(SKRect.Create(10 / 2, 10 / 2, info.Width - 10, info.Height - 10), 100, 100, Borderpaint);
            canvas.Save();
            canvas.Restore();
            float radius = Math.Min((float)info.Height / 2, (float)info.Height / 2);
            
            canvas.DrawCircle(CXtoCircle / 2, CYtoCircle / 2, radius - 10 / 2, Thumbpaint);
            canvas.Save();
            canvas.Restore();

        }
        protected override void OnPropertyChanging(string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);
            InvalidateSurface();
        }
        protected override void OnTouch(SKTouchEventArgs e)
        {
            base.OnTouch(e);
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    if (e.InContact)
                    {
                        IsToggled = !IsToggled;
                        OnToggledChanged(IsToggled);
                        e.Handled = true;

                        InvalidateSurface();
                    }
                    break;

            } 
        }
    }
}
