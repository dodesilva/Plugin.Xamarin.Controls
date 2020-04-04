using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.Interfaces;
using Plugin.Xamarin.Controls.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PXC_Button), typeof(PXC_ButtonRenderer))]
namespace Plugin.Xamarin.Controls.Droid.Controls
{
    [Preserve(AllMembers = true)]
    public class PXC_ButtonRenderer : ButtonRenderer
    {
        Context _context;
        public PXC_ButtonRenderer(Context context) : base(context)
        {
            _context = context;
        }
        
        protected override void OnElementChanged(ElementChangedEventArgs<global::Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                if ((int)Build.VERSION.SdkInt < 21)
                {
                    SetLayerType(LayerType.Software, null);
                }
            }

            var view = (PXC_Button)Element;
            //Control.SetPadding((int)view.Padding.Left, (int)view.Padding.Top, (int)view.Padding.Right, (int)view.Padding.Bottom);

            if (!String.IsNullOrEmpty(view.Icon))
            {
                Control.Typeface = Typeface.CreateFromAsset(_context.Assets, Helpers.Extensions.FindNameFileForFont(view.FontIconName));

                IIcon icon = Helpers.Extensions.FindIconForKey(view.Icon, view.FontIconName);
                if (string.IsNullOrEmpty(view.Text))
                {
                    view.Text = string.Format("{0}", icon.Character);
                }
                else
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
            else
            {
                Control.Typeface = Typeface.Create(Element.FontFamily, TypefaceStyle.Normal);
                view.Text = view.Text;
            }
        }
        
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (PXC_Button)Element;

            if ( e.PropertyName == PXC_Button.FontIconProperty.PropertyName ||e.PropertyName == PXC_Button.IconProperty.PropertyName )
            {
                //Control.SetPadding((int)view.Padding.Left, (int)view.Padding.Top, (int)view.Padding.Right, (int)view.Padding.Bottom);
                if (!String.IsNullOrEmpty(view.Icon))
                {
                    Control.Typeface = Typeface.CreateFromAsset(_context.Assets, Helpers.Extensions.FindNameFileForFont(view.FontIconName));

                    IIcon icon = Helpers.Extensions.FindIconForKey(view.Icon, view.FontIconName);
                    if (string.IsNullOrEmpty(view.Text))
                    {
                        view.Text = string.Format("{0}", icon.Character);
                    }
                    else
                    {
                        if (icon != null)
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
                else
                {
                    Control.Typeface = Typeface.Create(Element.FontFamily, TypefaceStyle.Normal);
                    view.Text = view.Text;
                }
                this.Invalidate();
            }
        }
    }
}