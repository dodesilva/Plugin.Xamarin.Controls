using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Widget;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Droid.Controls;
using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PXC_Label), typeof(PXC_LabelRenderer))]
namespace Plugin.Xamarin.Controls.Droid.Controls
{
    public class PXC_LabelRenderer : LabelRenderer
    {
        Context _context;
        public PXC_LabelRenderer(Context context) : base(context)
        {
            _context = context;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            var view = (PXC_Label)Element;
            if (Control != null)
            {
                
                if (!String.IsNullOrEmpty(view.Icon) || view.FontName != FontType.None)
                {
                    if (view.FontName != FontType.None && String.IsNullOrEmpty(view.Icon))
                    {
                        Control.Typeface = TrySetFont(view.FontName.ToString());
                    }
                    else if (view.FontName == FontType.None && !String.IsNullOrEmpty(view.Icon))
                    {
                        if (view.FontIconName != Fonts.None)
                        {
                            Control.Typeface = Typeface.CreateFromAsset(_context.Assets, Helpers.Extensions.FindNameFileForFont(view.FontIconName));

                            IIcon icon = Helpers.Extensions.FindIconForKey(view.Icon, view.FontIconName);
                            if (string.IsNullOrEmpty(view.Text))
                            {
                                if (icon != null)
                                {
                                    view.Text = string.Empty;
                                    view.Text = string.Format("{0}", icon.Character);
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
                                                view.Text = string.Format("{0} {1}", view.Text, icon.Character);
                                                break;
                                            case TextFloting.Right:
                                                view.Text = string.Format("{0} {1}", icon.Character, view.Text);
                                                break;
                                            default:
                                                view.Text = string.Format("{0} {1}", icon.Character, view.Text);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Toast.MakeText(_context, "Select FontIconName ou FontName,not all in same time", ToastLength.Long);
                    }
                }
                else
                {
                    Control.Typeface = Typeface.Create(Element.FontFamily, TypefaceStyle.Normal);
                    view.Text = view.Text;
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (PXC_Label)Element;
            if (e.PropertyName == PXC_Label.FontIconProperty.PropertyName ||
                e.PropertyName == PXC_Label.IconProperty.PropertyName||
                e.PropertyName == PXC_Label.FontNameProperty.PropertyName)
            {
               
                if (!String.IsNullOrEmpty(view.Icon) || view.FontName!=FontType.None)
                {
                    if (view.FontName != FontType.None && String.IsNullOrEmpty(view.Icon))
                    {
                        Control.Typeface = TrySetFont(view.FontName.ToString());
                    }
                    else if (view.FontName == FontType.None && !String.IsNullOrEmpty(view.Icon))
                    {
                        if (view.FontIconName != Fonts.None)
                        {
                            Control.Typeface = Typeface.CreateFromAsset(_context.Assets, Helpers.Extensions.FindNameFileForFont(view.FontIconName));

                            IIcon icon = Helpers.Extensions.FindIconForKey(view.Icon, view.FontIconName);
                            if (string.IsNullOrEmpty(view.Text))
                            {
                                if (icon != null)
                                {
                                    view.Text = string.Empty;
                                    view.Text = string.Format("{0}", icon.Character);
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
                                                view.Text = string.Format("{0} {1}", view.Text, icon.Character);
                                                break;
                                            case TextFloting.Right:
                                                view.Text = string.Format("{0} {1}", icon.Character, view.Text);
                                                break;
                                            default:
                                                view.Text = string.Format("{0} {1}", icon.Character, view.Text);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Toast.MakeText(_context, "Select FontIconName ou FontName,not all in same time", ToastLength.Long);
                    }
                }
                else
                {
                    Control.Typeface = Typeface.Create(Element.FontFamily, TypefaceStyle.Normal);
                    view.Text = view.Text;
                }
            }
        }

        private Typeface TrySetFont(string fontName)
        {
            string ext = string.Empty;
            try
            {
                if(fontName== "OpenSans_Bold" || fontName == "FFF_Tusj" || fontName == "OpenSans_Regular" || fontName == "architep")
                {
                    ext = ".ttf";
                }
                else
                {
                    ext = ".otf";
                }
                return Typeface.CreateFromAsset(Context.Assets, fontName+ext);
            }
            catch (Exception ex)
            {
                Console.WriteLine("not found in assets. Exception: {0}", ex);
                try
                {
                    return Typeface.CreateFromFile(fontName);
                }
                catch (Exception ex1)
                {
                    Console.WriteLine("not found by file. Exception: {0}", ex1);

                    return Typeface.Default;
                }
            }
        }
    }
}