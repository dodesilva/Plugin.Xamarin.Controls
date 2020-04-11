
using Foundation;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.Forms.IOS.Controls;
using Plugin.Xamarin.Controls.Interfaces;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PXC_Label), typeof(PXC_LabelRenderer))]
namespace Plugin.Xamarin.Controls.Forms.IOS.Controls
{
    public class PXC_LabelRenderer: LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            var view = Element as PXC_Label;
            if (view == null || Control == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(((PXC_Label)Element).Icon) || view.FontName != FontType.None)
            {
                if (string.IsNullOrEmpty(((PXC_Label)Element).Icon) || view.FontName != FontType.None)
                {

                    var font = UIFont.FromName(view.FontName.ToString(), this.Control.Font.PointSize);

                    if (font != null)
                    {
                        Control.Font = font;
                    }
                }
                else if (!string.IsNullOrEmpty(((PXC_Label)Element).Icon) || view.FontName == FontType.None)
                {
                    if (view.FontIconName != Fonts.None)
                    {
                        Control.Font = UIFont.FromName(Helpers.Extensions.FindNameForFont(((PXC_Label)Element).FontIconName), (nfloat)Element.FontSize);
                        IIcon icon = Helpers.Extensions.FindIconForKey(((PXC_Label)Element).Icon, ((PXC_Label)Element).FontIconName);
                        if (string.IsNullOrEmpty(view.Text))
                        {
                            if (icon != null)
                            {
                                Control.Text = string.Empty;
                                Control.Text = string.Format("{0}", icon.Character);
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
                                            Control.Text = string.Format("{0} {1}", view.Text, icon.Character);
                                            break;
                                        case TextFloting.Right:
                                            Control.Text = string.Format("{0} {1}", icon.Character, view.Text);
                                            break;
                                        default:
                                            Control.Text = string.Format("{0} {1}", icon.Character, view.Text);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Control.Font = Font.OfSize(Element.FontFamily, Element.FontSize).WithAttributes(Element.FontAttributes).ToUIFont();
                Control.Text=((PXC_Label)Element).Text;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (PXC_Label)Element;
            if (e.PropertyName == PXC_Label.FontIconProperty.PropertyName ||
                e.PropertyName == PXC_Label.IconProperty.PropertyName ||
                e.PropertyName == PXC_Label.FontNameProperty.PropertyName)
            {
                if (!string.IsNullOrEmpty(((PXC_Label)Element).Icon) || view.FontName != FontType.None)
                {
                    if (string.IsNullOrEmpty(((PXC_Label)Element).Icon) || view.FontName != FontType.None)
                    {

                        var font = UIFont.FromName(view.FontName.ToString(), this.Control.Font.PointSize);

                        if (font != null)
                        {
                            this.Control.Font = font;
                        }
                    }
                    else if (!string.IsNullOrEmpty(((PXC_Label)Element).Icon) || view.FontName == FontType.None)
                    {
                        if (view.FontIconName != Fonts.None)
                        {
                            Control.Font = UIFont.FromName(Helpers.Extensions.FindNameForFont(((PXC_Label)Element).FontIconName), (nfloat)Element.FontSize);
                            IIcon icon = Helpers.Extensions.FindIconForKey(((PXC_Label)Element).Icon, ((PXC_Label)Element).FontIconName);
                            if (string.IsNullOrEmpty(view.Text))
                            {
                                if (icon != null)
                                {
                                    Control.Text = string.Empty;
                                    Control.Text = string.Format("{0}", icon.Character);
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
                                                Control.Text = string.Format("{0} {1}", view.Text, icon.Character);
                                                break;
                                            case TextFloting.Right:
                                                Control.Text = string.Format("{0} {1}", icon.Character, view.Text);
                                                break;
                                            default:
                                                Control.Text = string.Format("{0} {1}", icon.Character, view.Text);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Control.Font = Font.OfSize(Element.FontFamily, Element.FontSize).WithAttributes(Element.FontAttributes).ToUIFont();
                    Control.Text = ((PXC_Label)Element).Text;
                }
            }
        }
    }
}