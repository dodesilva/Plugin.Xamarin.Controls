using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PXC_WebView), typeof(PXC_WebViewRenderer))]
namespace Plugin.Xamarin.Controls.Droid.Controls
{
    public class PXC_WebViewRenderer : WebViewRenderer
    {

        public PXC_WebViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var customWebView = Element as PXC_WebView;
                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                if (!customWebView.IsPdf)
                    Control.LoadUrl(customWebView.Uri);
                else
                {
                    var fileName = $"file:///{customWebView.Uri}";
                    Control.LoadUrl($"file:///android_asset/pdfjs/web/viewer.html?file={fileName}");
                }
            }
        }
    }
}