using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AndroidViews = Android.Views;

[assembly: ExportRenderer(typeof(PXC_Editor), typeof(PXC_EditorRenderer))]
namespace Plugin.Xamarin.Controls.Droid.Controls
{
    public class PXC_EditorRenderer : EditorRenderer
    {
        Context _context;
        public PXC_EditorRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                if ((int)Build.VERSION.SdkInt < 21)
                {
                    SetLayerType(LayerType.Software, null);
                }
            }
            var view = (PXC_Editor)Element;
            if (Control != null)
            {
                if (view.HasBorder)
                {
                    CreateBorderRadius(view);
                }

                if (!string.IsNullOrEmpty(view.FontName))
                {
                    Control.Typeface = TrySetFont(view.FontName);
                }
               
                GetTextAlignment(view.TextAlignment);
                Control.SetPadding((int)view.Padding.Left, (int)view.Padding.Top, (int)view.Padding.Right, (int)view.Padding.Bottom);
            }

        }

        private Typeface TrySetFont(string fontName)
        {
            try
            {
                return Typeface.CreateFromAsset(Context.Assets, fontName);
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

        private void GetTextAlignment(EditorAlign textAlignment)
        {
            switch (textAlignment)
            {
                case EditorAlign.Start:
                    Control.TextAlignment = AndroidViews.TextAlignment.TextStart;
                    Control.Gravity = GravityFlags.Start;
                    break;
                case EditorAlign.Center:
                    Control.TextAlignment = AndroidViews.TextAlignment.Center;
                    Control.Gravity = GravityFlags.Center;
                    break;
                case EditorAlign.End:
                    Control.TextAlignment = AndroidViews.TextAlignment.TextEnd;
                    Control.Gravity = GravityFlags.End;
                    break;
            }
        }

        private void CreateBorderRadius(PXC_Editor view)
        {

            var borderwidth = (float)view.BorderWidth;
            var density = _context.Resources.DisplayMetrics.Density;
            var radius = (float)Math.Ceiling(view.BorderRadius * density + .5f);
            var gradient = new GradientDrawable();

            int stocker = 0;
            if (borderwidth > 0)
            {

                stocker = (int)Math.Ceiling(borderwidth * density + .5f);
            }

            gradient.SetCornerRadius(radius);
            gradient.SetShape(ShapeType.Rectangle);
            gradient.SetStroke(stocker, view.BorderColor.ToAndroid());
            gradient.SetColor(view.FillBackGroungColor.ToAndroid());

            Control.SetBackground(gradient);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var view = (PXC_Editor)Element;

            if (e.PropertyName == PXC_Editor.BorderColorProperty.PropertyName ||
              e.PropertyName == PXC_Editor.BorderWidthProperty.PropertyName ||
              e.PropertyName == PXC_Editor.FontNameProperty.PropertyName ||
              e.PropertyName == PXC_Editor.BorderWidthProperty.PropertyName ||
              e.PropertyName == PXC_Editor.FillBackGroungColorProperty.PropertyName)
            {
                if (view.HasBorder)
                {
                    CreateBorderRadius(view);
                }

                if (!string.IsNullOrEmpty(view.FontName))
                {
                    Control.Typeface = TrySetFont(view.FontName);
                }

                Control.SetPadding((int)view.Padding.Left, (int)view.Padding.Top, (int)view.Padding.Right, (int)view.Padding.Bottom);
                this.Invalidate();
            }
        }
    }
}