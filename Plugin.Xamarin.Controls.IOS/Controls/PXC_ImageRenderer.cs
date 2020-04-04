using System;
using System.ComponentModel;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Forms.IOS.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PXC_Image), typeof(PXC_ImageRenderer))]
namespace Plugin.Xamarin.Controls.Forms.IOS.Controls
{
    public class PXC_ImageRenderer : ImageRenderer
    {
        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);
            }
            catch (NullReferenceException)
            {
                // Random bug in Dispose method
                // https://bugzilla.xamarin.com/show_bug.cgi?id=21457
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (Control == null || Element == null)
            {
                return;
            }

            CreateCircle();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                e.PropertyName == PXC_Image.BorderRadiusProperty.PropertyName ||
                e.PropertyName == VisualElement.WidthProperty.PropertyName ||
                e.PropertyName == PXC_Image.BorderColorProperty.PropertyName ||
                e.PropertyName == PXC_Image.BorderWidthProperty.PropertyName ||
                e.PropertyName == PXC_Image.FillBackGroungColorProperty.PropertyName)
            {
                CreateCircle();
            }
        }

        private void CreateCircle()
        {
            if (((PXC_Image)Element).IsCircle)
            {
                var min = Math.Min(Element.Width, Element.Height);
                Control.Layer.CornerRadius = (float)(min / 2.0);
                Control.Layer.MasksToBounds = false;
                Control.Layer.BorderColor = ((PXC_Image)Element).BorderColor.ToCGColor();
                Control.Layer.BorderWidth = ((PXC_Image)Element).BorderWidth;
                Control.BackgroundColor = ((PXC_Image)Element).FillBackGroungColor.ToUIColor();
                Control.ClipsToBounds = true;
            }
            else
            {
                Control.Layer.CornerRadius = ((PXC_Image)Element).BorderRadius;
                Control.Layer.MasksToBounds = false;
                Control.Layer.BorderColor = ((PXC_Image)Element).BorderColor.ToCGColor();
                Control.Layer.BorderWidth = ((PXC_Image)Element).BorderWidth;
                Control.BackgroundColor = ((PXC_Image)Element).FillBackGroungColor.ToUIColor();
                Control.ClipsToBounds = true;
            }
        }
    }
}