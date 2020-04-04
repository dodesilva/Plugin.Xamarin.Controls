using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.IOS.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PXC_WebView), typeof(PXC_WebViewRenderer))]
namespace Plugin.Xamarin.Controls.IOS.Controls
{
    public class PXC_WebViewRenderer : ViewRenderer<PXC_WebView, UIWebView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<PXC_WebView> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				SetNativeControl(new UIWebView());
			}
			if (e.OldElement != null)
			{
				// Cleanup
			}
			if (e.NewElement != null)
			{
				var customWebView = Element as PXC_WebView;

				if (string.IsNullOrEmpty(customWebView.Uri))
					return;

				var url = NSUrl.FromFilename(customWebView.Uri);
				Control.LoadRequest(new NSUrlRequest(url));
				Control.ScalesPageToFit = true;
			}
		}
	}
}