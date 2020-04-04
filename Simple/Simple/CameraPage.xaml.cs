using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.EventArgsFile;
using Plugin.Xamarin.Controls.Helpers;
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
    public partial class CameraPage : ContentPage
    {
        public CameraPage()
        {
            InitializeComponent();
            instance = this;
            BindingContext = this;
            IsVideo = false;
            IsImage = false;
        }

        private bool ispaused = false;

        #region Singleton
        private static CameraPage instance;

        public static CameraPage GetInstance()
        {
            if (instance == null)
            {
                return new CameraPage();
            }

            return instance;
        }
        #endregion

        private ImageSource _image;
        public ImageSource Imagevideo
        {
            get { return _image; }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged(nameof(Imagevideo));
                }
            }
        }

        private string _filesource;
        public string FileSource
        {
            get { return _filesource; }
            set
            {
                if (_filesource != value)
                {
                    _filesource = value;
                    OnPropertyChanged(nameof(FileSource));
                }
            }
        }
       

        private bool _isvideoVisible;
        public bool IsVideo
        {
            get { return _isvideoVisible; }
            set
            {
                if (_isvideoVisible != value)
                {
                    _isvideoVisible = value;
                    OnPropertyChanged(nameof(IsVideo));
                }
            }
        }
        private bool _isimage;
        public bool IsImage
        {
            get { return _isimage; }
            set
            {
                if (_isimage != value)
                {
                    _isimage = value;
                    OnPropertyChanged(nameof(IsImage));
                }
            }
        }

        public void setVideoOrImage(MediaFiles mediaFiles)
        {

            if (mediaFiles.Success)
            {

                if (mediaFiles.MimeType.Contains("video"))
                {
                    FileSource = mediaFiles.FullPath;
                    IsVideo = true;
                    IsImage = false;
                   
                }
                else
                {
                    IsVideo = false;
                    IsImage = true;
                    Imagevideo = mediaFiles.FileSource;
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Cameraview());
        }
       
    }
}