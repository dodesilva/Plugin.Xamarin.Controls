using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.IOS.Classes;
using Plugin.Xamarin.Controls.IOS.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PXC_Camera), typeof(PXC_CameraRenderer))]
namespace Plugin.Xamarin.Controls.IOS.Controls
{
    public class PXC_CameraRenderer : ViewRenderer<PXC_Camera, CameraIOSView>
    {
        CameraIOSView cameraIOS;
       
        protected override void OnElementChanged(ElementChangedEventArgs<PXC_Camera> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                cameraIOS.OnFinichedCaptur -= CameraIOS_OnFinichedCaptur;
            }
            if(Control==null && e.NewElement != null)
            {
                cameraIOS = new CameraIOSView(Element.Camera);
                SetNativeControl(cameraIOS);
            }
        }

        private void CameraIOS_OnFinichedCaptur(object sender, Helpers.MediaFiles e)
        {
            (Element as PXC_Camera).SetPhotoResult(e);
        }
    }
}