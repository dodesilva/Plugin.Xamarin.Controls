﻿using System;
using CoreGraphics;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.IOS.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PXC_Picker), typeof(PXC_PickerRenderer))]
namespace Plugin.Xamarin.Controls.IOS.Controls
{
    public class PXC_PickerRenderer: PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            var view = (PXC_Picker)Element;
            if (e.NewElement != null && Element != null)
            {
                
                Control.LeftView = new UIView(new CGRect(0f, 0f, 9f, 20f));
                Control.LeftViewMode = UITextFieldViewMode.Always;
                if (!string.IsNullOrEmpty(view.Image))
                {
                    var downarrow = UIImage.FromBundle(view.Image);
                    Control.RightViewMode = UITextFieldViewMode.Always;
                    Control.RightView = new UIImageView(downarrow);
                }
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