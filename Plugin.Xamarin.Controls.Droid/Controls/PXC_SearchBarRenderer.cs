using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PXC_SearchBar), typeof(PXC_SearchBarRenderer))]
namespace Plugin.Xamarin.Controls.Droid.Controls
{
    public class PXC_SearchBarRenderer : SearchBarRenderer
    {
        Context _context;
        public PXC_SearchBarRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                if ((int)Build.VERSION.SdkInt < 21)
                {
                    SetLayerType(LayerType.Software, null);
                }
            }
            var view = (PXC_SearchBar)Element;
            if (Control != null)
            {
                if (view.HasBorder)
                {
                    var borderwidth = (float)view.BorderWidth;
                    var density = _context.Resources.DisplayMetrics.Density;
                    var radius = (float)Math.Ceiling(view.BorderRadius * density + .5f);
                    var gradient = new GradientDrawable();

                    int stocker = 0;
                    if (borderwidth > 0)
                    {

                        stocker = (int)Math.Ceiling(borderwidth * density + .5f);
                    }

                    gradient.SetCornerRadius(radius);
                    gradient.SetStroke(stocker, view.BorderColor.ToAndroid());
                    gradient.SetColor(view.FillBackGroungColor.ToAndroid());

                    Control.SetBackground(gradient);
                }
                //Control.SetPadding((int)view.Padding.Left, (int)view.Padding.Top, (int)view.Padding.Right, (int)view.Padding.Bottom);

            }

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var view = (PXC_SearchBar)Element;
            if (e.PropertyName == PXC_SearchBar.HasBorderProperty.PropertyName)
            {
                if (view.HasBorder)
                {
                    var borderwidth = (float)view.BorderWidth;
                    var density = _context.Resources.DisplayMetrics.Density;
                    var radius = (float)Math.Ceiling(view.BorderRadius * density + .5f);
                    var gradient = new GradientDrawable();

                    int stocker = 0;
                    if (borderwidth > 0)
                    {

                        stocker = (int)Math.Ceiling(borderwidth * density + .5f);
                    }

                    gradient.SetCornerRadius(radius);
                    gradient.SetStroke(stocker, view.BorderColor.ToAndroid());
                    gradient.SetColor(view.FillBackGroungColor.ToAndroid());

                    Control.SetBackground(gradient);
                }
            }
        }
    }
}