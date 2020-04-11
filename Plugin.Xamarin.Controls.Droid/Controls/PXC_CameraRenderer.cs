using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Droid.Classes;
using Plugin.Xamarin.Controls.Droid.Controls;
using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PXC_Camera), typeof(PXC_CameraRenderer))]
namespace Plugin.Xamarin.Controls.Droid.Controls
{
    public class PXC_CameraRenderer : ViewRenderer<PXC_Camera, CameraDroidView>
    {

        private Context _context;
        private CameraDroidView _cameraDroidView;
        Activity ActivityResult => this.Context as Activity; 

        public PXC_CameraRenderer(Context context) : base(context)
        {
            _context = context;

        }
        protected override void OnElementChanged(ElementChangedEventArgs<PXC_Camera> e)
        {
            base.OnElementChanged(e);
            if (Control == null && e.NewElement != null)
            {
                    _cameraDroidView = new CameraDroidView(Context, Element.Camera);
                    SetNativeControl(_cameraDroidView);
                    _cameraDroidView.OnFinichedCapture += _cameraDroidView_OnFinichedCapture;
            }
        }

        private void _cameraDroidView_OnFinichedCapture(object sender, MediaFiles e)
        {

            (Element as PXC_Camera).SetPhotoResult(e);
        }

    }
}