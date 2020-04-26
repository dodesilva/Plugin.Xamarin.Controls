using System;
using System.ComponentModel;
using Plugin.Xamarin.Controls;
using Foundation;
using Plugin.Xamarin.Controls.Forms.IOS.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Plugin.Xamarin.Controls.Interfaces;
using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.FontFile;

[assembly: ExportRenderer(typeof(PXC_Button), typeof(PXC_ButtonRenderer))]
namespace Plugin.Xamarin.Controls.Forms.IOS.Controls
{
    [Preserve(AllMembers = true)]
    public class PXC_ButtonRenderer : ButtonRenderer
    {
        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);
            }
            catch (NullReferenceException)
            {
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<global::Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (Control == null || Element == null)
                throw new InvalidOperationException(String.Format("Cannot convert {0} into {1}", Element.Text, typeof(Icon)));         
            var view = (PXC_Button)Element;
            if (!string.IsNullOrEmpty(((PXC_Button)Element).Icon))
            {
                if (view.FontIconName != Fonts.None)
                {
                    Control.Font = UIFont.FromName(Helpers.Extensions.FindNameForFont(((PXC_Button)Element).FontIconName), (nfloat)Element.FontSize);
                    IIcon icon = Helpers.Extensions.FindIconForKey(((PXC_Button)Element).Icon, ((PXC_Button)Element).FontIconName);
                    if (string.IsNullOrEmpty(view.Text))
                    {
                        if (icon != null)
                        {
                            Control.SetTitle("", UIControlState.Normal);
                            Control.SetTitle(string.Format("{0}", icon.Character), UIControlState.Normal);
                        }
                    }
                    else
                    {
                        if (icon != null)
                        {
                            if (!view.Text.Contains(icon.Character))
                            {
                                switch (view.TextFloat)
                                {
                                    case TextFloting.Left:
                                        Control.SetTitle(string.Format("{0} {1}", view.Text, icon.Character), UIControlState.Normal);
                                        break;
                                    case TextFloting.Right:
                                        Control.SetTitle(string.Format("{0} {1}", icon.Character, view.Text), UIControlState.Normal);
                                        break;
                                    default:
                                        Control.SetTitle(string.Format("{0} {1}", icon.Character, view.Text), UIControlState.Normal);
                                        break;
                                }
                            }
                        }
                    }
                }           
            }
            else
            {
                Control.Font = Font.OfSize(Element.FontFamily, Element.FontSize).WithAttributes(Element.FontAttributes).ToUIFont();
                Control.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
                Control.TitleLabel.TextAlignment = UITextAlignment.Center;
                Control.SetTitle(((PXC_Button)Element).Text, UIControlState.Normal);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control == null || Element == null)
                return;
            if (e.PropertyName == PXC_Button.IconProperty.PropertyName ||e.PropertyName == PXC_Button.FontIconProperty.PropertyName)
            {
                var view = (PXC_Button)Element;
                if (!string.IsNullOrEmpty(((PXC_Button)Element).Icon))
                {
                    Control.Font = UIFont.FromName(Helpers.Extensions.FindNameForFont(((PXC_Button)Element).FontIconName), (nfloat)Element.FontSize);
                    IIcon icon = Helpers.Extensions.FindIconForKey(((PXC_Button)Element).Icon, ((PXC_Button)Element).FontIconName);
                    if (string.IsNullOrEmpty(view.Text))
                    {
                        if (icon != null)
                        {
                            Control.SetTitle("", UIControlState.Normal);
                            Control.SetTitle(string.Format("{0}", icon.Character), UIControlState.Normal);
                        }
                    }
                    else
                    {
                        if (icon != null)
                        {
                            if (!view.Text.Contains(icon.Character))
                            {
                                switch (view.TextFloat)
                                {
                                    case TextFloting.Left:
                                        Control.SetTitle(string.Format("{0} {1}", view.Text, icon.Character), UIControlState.Normal);
                                        break;
                                    case TextFloting.Right:
                                        Control.SetTitle(string.Format("{0} {1}", icon.Character, view.Text), UIControlState.Normal);
                                        break;
                                    default:
                                        Control.SetTitle(string.Format("{0} {1}", icon.Character, view.Text), UIControlState.Normal);
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    Control.Font = Font.OfSize(Element.FontFamily, Element.FontSize).WithAttributes(Element.FontAttributes).ToUIFont();
                    Control.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
                    Control.TitleLabel.TextAlignment = UITextAlignment.Center;
                    Control.SetTitle(((PXC_Button)Element).Text, UIControlState.Normal);
                }
            }
        }
    }
}