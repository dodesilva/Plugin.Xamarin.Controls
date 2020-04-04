using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.EventArgsFile;
using Simple.ViewModel;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Simple
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageVideo : ContentPage
    {
        private bool ispaused = false;
        public PageVideo()
        {            
            InitializeComponent();
            //BindingContext = new VideoViewModel();
            VideoPlayer.FileSource = "https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";
            ImgVolume.Source = "ic_pause_circle_outline.png";
            ImgVolume.Source = "ic_volume.png";
        }

        private void VideoPlayer_VideoVolumeChanged(object sender, VolumeFileChangedEventArgs e)
        {
            if (e.Muted)
            {
                ImgVolume.Source = "ic_volume_off.png";
            }
            else
            {
                ImgVolume.Source = "ic_volume.png";
            }
        }

        private void SetProgressHide()
        {
            var translateLength = 400u;
            Task.Run(async() =>
            {
                gridToDisplay.FadeTo(1, 400u, Easing.SinInOut);
                await Task.Delay(3000);
                if (ispaused == false)
                {
                    isclied = false;
                    await gridToDisplay.FadeTo(0, translateLength, Easing.SinInOut);
                }
            });
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width > height)
            {
                VideoPlayer.WidthRequest = width;
                VideoPlayer.HeightRequest = height;
                VideoPlayer.OrientationChanged(true);
                gridToDisplay.WidthRequest = width-60;
                gridToDisplay.HeightRequest = height-30;
            }
            else
            {
                VideoPlayer.WidthRequest = width;
                VideoPlayer.HeightRequest = 250;
                gridToDisplay.WidthRequest = width-40;
                gridToDisplay.HeightRequest = 230;
                VideoPlayer.OrientationChanged(false);
            }
        }

        bool isclied = false;
        private void PXC_StackLayout_Clicked(object sender, EventArgs e)
        {
            if (isclied)
            {
                if (ispaused==false)
                {
                    gridToDisplay.FadeTo(0, 400u, Easing.SinInOut);
                    isclied = false;
                }
            }
            else
            {
                SetProgressHide();
                isclied = true;
            }
        }
        public string GetFormattedTime(int value)
        {
            var span = TimeSpan.FromMilliseconds(value);
            if (span.Hours > 0)
            {
                return string.Format("{0}:{1:00}:{2:00}", (int)span.TotalHours, span.Minutes, span.Seconds);
            }
            else
            {
                return string.Format("{0}:{1:00}", (int)span.Minutes, span.Seconds);
            }
        }

        private void VideoPlayer_VideoProgressChanged(object sender, ProgressFileChangedEventArgs e)
        {
            position.Text= GetFormattedTime((int)e.Position.TotalMilliseconds);
            duration.Text= GetFormattedTime((int)e.Duration.TotalMilliseconds);
        }

        private void VideoPlayer_VideoStatusChanged(object sender, StatusFileChangedEventArgs e)
        {
            switch (e.Status)
            {
                case VideoStatus.Loading:
                    ImgPlay.Source = "ic_play_circle_outline.png";
                    gridToDisplay.FadeTo(1, 400u, Easing.SinInOut);
                    ispaused = true;
                    break;
                case VideoStatus.Paused:
                    ImgPlay.Source = "ic_play_circle_outline.png";
                    gridToDisplay.FadeTo(1, 400u, Easing.SinInOut);
                    ispaused = true;
                    break;
                case VideoStatus.Playing:
                    ImgPlay.Source = "ic_pause_circle_outline.png";
                    ispaused = false;
                    SetProgressHide();
                    break;
                case VideoStatus.Stopped:
                    ImgPlay.Source = "ic_play_circle_outline.png";
                    gridToDisplay.FadeTo(1, 400u, Easing.SinInOut);
                    ispaused = true;
                    break;
            }
        }

        private void VideoPlayer_VideoFinishedChanged(object sender, FinishedFileEventArgs e)
        {
            ImgPlay.Source = "ic_play_circle_outline.png";
            gridToDisplay.FadeTo(1, 400u, Easing.SinInOut);
            ispaused = true;
            isclied = true;
        }
        //private void VideoPlayer_FullScreenStatusChanged(object sender, bool e)
        //{
        //    Imgfull.Source = e ? "ic_fullscreen_exit.png" : "ic_crop_free.png";
        //}
    }
}