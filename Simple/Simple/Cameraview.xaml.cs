using Plugin.Xamarin.Controls.EventArgsFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Simple
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cameraview : ContentPage
    {
        public Cameraview()
        {
            InitializeComponent();
        }

        private async void PXC_Camera_OnRecordOrPhotoResult(ResultEventArgs result)
        {
            await Navigation.PopModalAsync();
            var main = CameraPage.GetInstance();
            main.setVideoOrImage(result.MediaFiles);
        }
    }
}