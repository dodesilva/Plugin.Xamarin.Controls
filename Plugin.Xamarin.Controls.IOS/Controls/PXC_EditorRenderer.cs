using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Forms.IOS.Controls;
using System;
using System.ComponentModel;
using System.IO;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PXC_Editor), typeof(PXC_EditorRenderer))]
namespace Plugin.Xamarin.Controls.Forms.IOS.Controls
{
    public class PXC_EditorRenderer : EditorRenderer
    {
        private string Placeholder { get; set; }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            var view = (PXC_Editor)Element;
            base.OnElementChanged(e);
            if (Control != null && view != null)
            {
                CreatePlaceHolder(view);

                GetTextAlignment(view.TextAlignment);

                Control.TextContainerInset = new UIEdgeInsets((int)view.Padding.Top, (int)view.Padding.Left, (int)view.Padding.Bottom, (int)view.Padding.Right);
                Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
                Control.ReturnKeyType = UIReturnKeyType.Done;
                // Radius for the curves  
                Control.Layer.CornerRadius = Convert.ToSingle(view.BorderRadius);
                // Thickness of the Border Color  
                Control.Layer.BorderColor = view.BorderColor.ToCGColor();
                // Thickness of the Border Width  
                Control.Layer.BorderWidth = view.BorderWidth;
                Control.ClipsToBounds = true;

                if (!string.IsNullOrEmpty(view.FontName))
                {
                    var fontName = Path.GetFileNameWithoutExtension(view.FontName);

                    var font = UIFont.FromName(fontName, this.Control.Font.PointSize);

                    if (font != null)
                    {
                        this.Control.Font = font;
                    }
                }
            }
        }

        private void CreatePlaceHolder(PXC_Editor view)
        {
            Placeholder = view.Placeholder;
            Control.TextColor = view.PlaceholderColor.ToUIColor();
            Control.Text = Placeholder;

            Control.ShouldBeginEditing += (UITextView textView) =>
            {
                if (textView.Text == Placeholder)
                {
                    textView.Text = "";
                    textView.TextColor = UIColor.Black; // Text Color
                }

                return true;
            };

            Control.ShouldEndEditing += (UITextView textView) =>
            {
                if (textView.Text == "")
                {
                    textView.Text = Placeholder;
                    textView.TextColor = view.PlaceholderColor.ToUIColor(); // Placeholder Color
                }

                return true;
            };
        }

        private void GetTextAlignment(EditorAlign textAlignment)
        {
            switch (textAlignment)
            {
                case EditorAlign.Start:
                    Control.TextAlignment = UITextAlignment.Left;
                    break;
                case EditorAlign.Center:
                    Control.TextAlignment = UITextAlignment.Center;
                    Control.Center = Center;
                    break;
                case EditorAlign.End:
                    Control.TextAlignment = UITextAlignment.Right;
                    break;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var view = (PXC_Editor)Element;
            if (e.PropertyName == PXC_Editor.FontNameProperty.PropertyName)
            {
                if (!string.IsNullOrEmpty(view.FontName))
                {
                    var fontName = Path.GetFileNameWithoutExtension(view.FontName);

                    var font = UIFont.FromName(fontName, this.Control.Font.PointSize);

                    if (font != null)
                    {
                        this.Control.Font = font;
                    }
                }
            }            
        }
    }
}