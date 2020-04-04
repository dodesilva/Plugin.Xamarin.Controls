using System;
using CoreGraphics;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Forms.IOS.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PXC_Entry), typeof(PXC_EntryRenderer))]
namespace Plugin.Xamarin.Controls.Forms.IOS.Controls
{
    public class PXC_EntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var view = (PXC_Entry)Element;
                Control.LeftView = new UIView(new CGRect(0f, 0f, 9f, 20f));
                Control.LeftViewMode = UITextFieldViewMode.Always;

                Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
                Control.ReturnKeyType = UIReturnKeyType.Done;
                // Radius for the curves  
                Control.Layer.CornerRadius = Convert.ToSingle(view.BorderRadius);
                // Thickness of the Border Color  
                Control.Layer.BorderColor = view.BorderColor.ToCGColor();
                // Thickness of the Border Width  
                Control.Layer.BorderWidth = view.BorderWidth;
                Control.Layer.BackgroundColor = view.FillBackGroungColor.ToCGColor();
                Control.ClipsToBounds = true;
            }
        }
    }
}