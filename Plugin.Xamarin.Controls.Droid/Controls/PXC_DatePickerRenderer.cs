using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.Content;
using Android.Views;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PXC_DatePicker), typeof(PXC_DatePickerRenderer))]
namespace Plugin.Xamarin.Controls.Droid.Controls
{
    public class PXC_DatePickerRenderer : DatePickerRenderer
    {
        Context _context;
        public PXC_DatePickerRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<global::Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                if ((int)Build.VERSION.SdkInt < 21)
                {
                    SetLayerType(LayerType.Software, null);
                }
            }

            var view = (PXC_DatePicker)Element;

            if (Control != null && this.Element != null)
            {
                if (!string.IsNullOrEmpty(view.Image))
                {
                    Control.SetBackground(AddPickerStyles(view));
                }
                else
                {
                    if (view.HasBorder)
                    {
                        Control.SetBackground(BorderBackGround(view));
                    }
                }
                Control.SetPadding((int)view.Padding.Left, (int)view.Padding.Top, (int)view.Padding.Right, (int)view.Padding.Bottom);
            }
        }
        private Drawable BorderBackGround(PXC_DatePicker view)
        {
            var borderwidth = (float)view.BorderWidth;
            int stocker = 0;

            if (borderwidth > 0)
            {
                var density = _context.Resources.DisplayMetrics.Density;
                stocker = (int)Math.Ceiling(borderwidth * density + .5f);
            }

            float radius = (float)view.BorderRadius + .0f;

            GradientDrawable border = new GradientDrawable();
            border.SetColor(view.FillBackGroungColor.ToAndroid());
            border.SetCornerRadius(radius);
            border.SetShape(ShapeType.Rectangle);
            border.SetStroke(stocker, view.BorderColor.ToAndroid());

            return border;
        }

        public LayerDrawable AddPickerStyles(PXC_DatePicker view)
        {

            var borderwidth = (float)view.BorderWidth;
            int stocker = 0;

            if (borderwidth > 0)
            {
                var density = _context.Resources.DisplayMetrics.Density;
                stocker = (int)Math.Ceiling(borderwidth * density + .5f);
            }

            float radius = (float)view.BorderRadius + .0f;

            GradientDrawable border = new GradientDrawable();
            if (view.HasBorder)
            {
                border.SetColor(view.FillBackGroungColor.ToAndroid());
                border.SetCornerRadius(radius);
                border.SetShape(ShapeType.Rectangle);
                border.SetStroke(stocker, view.BorderColor.ToAndroid());
            }
            Drawable[] layers = { border, GetDrawable(view.Image) };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

            return layerDrawable;
        }

        private BitmapDrawable GetDrawable(string imagePath)
        {
            int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 80, 70, true));
            result.Gravity = GravityFlags.Right;

            return result;
        }
    }
}