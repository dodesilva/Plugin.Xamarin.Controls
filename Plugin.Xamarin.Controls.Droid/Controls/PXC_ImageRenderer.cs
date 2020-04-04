using System;
using Android.Content;
using Android.Graphics;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AndroidViews = Android.Views;

[assembly: ExportRenderer(typeof(PXC_Image), typeof(PXC_ImageRenderer))]
namespace Plugin.Xamarin.Controls.Droid.Controls
{
    public class PXC_ImageRenderer:ImageRenderer
    {
        private Context _context;
        public PXC_ImageRenderer(Context context):base(context)
        {
            _context = context;
        }

        protected override bool DrawChild(Canvas canvas, AndroidViews.View child, long drawingTime)
        {
            var paint = new Paint();
            var path = new Path();
            float radius, borderThickness;
            int strokeWidth = 0;
            bool result;

            var density = _context.Resources.DisplayMetrics.Density;

            if (((PXC_Image)Element).IsCircle)
            {
                radius = Math.Min(Width, Height) / 2;

                borderThickness = (float)((PXC_Image)Element).BorderWidth;

                if (borderThickness > 0)
                {
                    strokeWidth = (int)Math.Ceiling(borderThickness * density + .5f);
                }

                radius -= strokeWidth / 2;
                
                path.AddCircle(Width / 2.0f, Height / 2.0f, radius, Path.Direction.Ccw);

                canvas.Save();
                canvas.ClipPath(path);

                paint.AntiAlias = true;
                paint.SetStyle(Paint.Style.Fill);
                paint.Color = ((PXC_Image)Element).FillBackGroungColor.ToAndroid();
                canvas.DrawPath(path, paint);
                paint.Dispose();
                
                result = base.DrawChild(canvas, child, drawingTime);

                path.Dispose();
                canvas.Restore();

                path = new Path();
                path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);
                
                if (strokeWidth > 0.0f)
                {
                    paint = new Paint();
                    paint.AntiAlias = true;
                    paint.StrokeWidth = strokeWidth;
                    paint.SetStyle(Paint.Style.Stroke);
                    paint.Color = ((PXC_Image)Element).BorderColor.ToAndroid();
                    canvas.DrawPath(path, paint);
                    paint.Dispose();
                }

                path.Dispose();
                return result;
            }

            if(!((PXC_Image)Element).IsCircle)
            {
                borderThickness = (float)((PXC_Image)Element).BorderWidth;

                if (borderThickness > 0)
                {
                    strokeWidth = (int)Math.Ceiling(borderThickness * density + .5f);
                }

                radius = (float)Math.Ceiling(((PXC_Image)Element).BorderRadius * density + .5f);

                path.AddRoundRect(new RectF(0, 0, Width, Height), radius, radius, Path.Direction.Ccw);

                canvas.Save();
                canvas.ClipPath(path);

                paint.AntiAlias = true;
                paint.SetStyle(Paint.Style.Fill);
                paint.Color = ((PXC_Image)Element).FillBackGroungColor.ToAndroid();
                canvas.DrawPath(path, paint);
                paint.Dispose();

                result = base.DrawChild(canvas, child, drawingTime);

                path.Dispose();
                canvas.Restore();

                path = new Path();
                path.AddRoundRect(new RectF(0, 0, Width, Height), radius, radius, Path.Direction.Ccw);

                if (strokeWidth > 0.0f)
                {
                    paint = new Paint();
                    paint.AntiAlias = true;
                    paint.StrokeWidth = strokeWidth;
                    paint.SetStyle(Paint.Style.Stroke);
                    paint.Color = ((PXC_Image)Element).BorderColor.ToAndroid();
                    canvas.DrawPath(path, paint);
                    paint.Dispose();
                }

                path.Dispose();
                return result;
            }

            return base.DrawChild(canvas, child, drawingTime);
        }
    }
}