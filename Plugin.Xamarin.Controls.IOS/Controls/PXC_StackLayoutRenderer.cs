using System;
using System.ComponentModel;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Forms.IOS.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PXC_StackLayout), typeof(PXC_StackLayoutRenderer))]
namespace Plugin.Xamarin.Controls.Forms.IOS.Controls
{
    public class PXC_StackLayoutRenderer : VisualElementRenderer<StackLayout>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;
            CreateBorderRadius();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                e.PropertyName == PXC_StackLayout.BorderRadiusProperty.PropertyName ||
                e.PropertyName == VisualElement.WidthProperty.PropertyName ||
                e.PropertyName == PXC_StackLayout.BorderColorProperty.PropertyName ||
                e.PropertyName == PXC_StackLayout.BorderWidthProperty.PropertyName ||
                e.PropertyName == PXC_StackLayout.FillBackGroungColorProperty.PropertyName)
            {
                CreateBorderRadius();
            }
        }

        private void CreateBorderRadius()
        {
            try
            {
                Layer.CornerRadius = ((PXC_StackLayout)Element).BorderRadius;
                Layer.MasksToBounds = false;
                Layer.BorderColor = ((PXC_StackLayout)Element).BorderColor.ToCGColor();
                Layer.BorderWidth = ((PXC_StackLayout)Element).BorderWidth;
                BackgroundColor = ((PXC_StackLayout)Element).FillBackGroungColor.ToUIColor();
                ClipsToBounds = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to create circle image: " + ex);
            }
        }
    }
}