using System;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Forms.IOS.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PXC_SearchBar), typeof(PXC_SearchBarRenderer))]
namespace Plugin.Xamarin.Controls.Forms.IOS.Controls
{
    public class PXC_SearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;
            CreateBorderRadius();
        }

        private void CreateBorderRadius()
        {
            try
            {
                Control.Layer.CornerRadius = ((PXC_SearchBar)Element).BorderRadius;
                Control.Layer.MasksToBounds = false;
                Control.Layer.BorderColor = ((PXC_SearchBar)Element).BorderColor.ToCGColor();
                Control.Layer.BorderWidth = ((PXC_SearchBar)Element).BorderWidth;
                Control.BackgroundColor = ((PXC_SearchBar)Element).FillBackGroungColor.ToUIColor();
                Control.ClipsToBounds = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to create corner: " + ex);
            }
        }
    }
}