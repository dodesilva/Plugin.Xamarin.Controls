using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PXC_StackLayout), typeof(PXC_StackLayoutRenderer))]
namespace Plugin.Xamarin.Controls.Droid.Controls
{
    public class PXC_StackLayoutRenderer : VisualElementRenderer<StackLayout>
    {
        Context _context;
        public PXC_StackLayoutRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                if ((int)Build.VERSION.SdkInt < 21)
                {
                    SetLayerType(LayerType.Software, null);
                }
            }
            var view = (PXC_StackLayout)Element;
            var logicalDensity = _context.Resources.DisplayMetrics.Density;
            var borderwidth = (float)view.BorderWidth;
            var radius = (float)Math.Ceiling(view.BorderRadius * logicalDensity + .5f);
            var gradient = new GradientDrawable();

            int stocker = 0;
            if (borderwidth > 0)
            {
                stocker = (int)Math.Ceiling(borderwidth * logicalDensity + .5f);
            }

            gradient.SetCornerRadius(radius);
            gradient.SetStroke(stocker, view.BorderColor.ToAndroid());
            gradient.SetColor(view.FillBackGroungColor.ToAndroid());

            SetBackgroundDrawable(gradient);
        }
    }
}