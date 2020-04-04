
using Foundation;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Forms.IOS.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PXC_Label_UnderLine), typeof(PXC_Label_UnderLineRenderer))]
namespace Plugin.Xamarin.Controls.Forms.IOS.Controls
{
    public class PXC_Label_UnderLineRenderer: LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            var label = Element as PXC_Label_UnderLine;
            if (label == null || Control == null)
            {
                return;
            }

            Control.Lines = label.MaxLines;
            if (label.IsUnderline)
            {
                var underline = label.IsUnderline ? NSUnderlineStyle.Single : NSUnderlineStyle.None;
                Control.AttributedText = new NSMutableAttributedString(label.Text, underlineStyle: underline);
            }

            if (!string.IsNullOrEmpty(label.FontName.ToString()))
            {
                
                var font = UIFont.FromName(label.FontName.ToString(), this.Control.Font.PointSize);

                if (font != null)
                {
                    this.Control.Font = font;
                }
            }

        }
    }
}