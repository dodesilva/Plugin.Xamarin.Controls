using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PXC_Label_UnderLine), typeof(PXC_Label_UnderLineRenderer))]
namespace Plugin.Xamarin.Controls.Droid.Controls
{
    public class PXC_Label_UnderLineRenderer : LabelRenderer
    {
        Context _context;
        public PXC_Label_UnderLineRenderer(Context context) : base(context)
        {
            _context = context;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var view = (PXC_Label_UnderLine)Element;
                if (view.MaxLines > 0)
                {
                    Control.SetMaxLines(view.MaxLines);
                }

                if (view.IsUnderline)
                {
                    Control.PaintFlags = Control.PaintFlags | PaintFlags.UnderlineText;
                }

                if (!string.IsNullOrEmpty(view.FontName.ToString()))
                {
                    Control.Typeface = TrySetFont(view.FontName.ToString());
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (PXC_Label_UnderLine)Element;

            switch (e.PropertyName)
            {
                case "MaxLines":
                    if (view.MaxLines > 0)
                    {
                        Control.SetMaxLines(view.MaxLines);
                    }
                    break;
                case "IsUnderline":
                    if (view.IsUnderline)
                    {
                        Control.PaintFlags = Control.PaintFlags | PaintFlags.UnderlineText;
                    }
                    break;
                case "FontName":
                    if (!string.IsNullOrEmpty(view.FontName.ToString()))
                    {
                        Control.Typeface = TrySetFont(view.FontName.ToString());
                    }
                    break;
                
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