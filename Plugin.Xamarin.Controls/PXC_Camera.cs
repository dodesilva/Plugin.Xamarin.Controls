using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.EventArgsFile;
using Plugin.Xamarin.Controls.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_Camera : View
    {
        public delegate void PhotoResultEventHandler(ResultEventArgs result);

        public event PhotoResultEventHandler OnRecordOrPhotoResult;

        public static readonly BindableProperty OnRecordOrPhotoResultCommandProperty = BindableProperty.Create(nameof(OnRecordOrPhotoResultCommand),
            typeof(ICommand), typeof(PXC_Camera));
        public ICommand OnRecordOrPhotoResultCommand
        {
            get { return (ICommand)GetValue(OnRecordOrPhotoResultCommandProperty); }
            set { SetValue(OnRecordOrPhotoResultCommandProperty, value); }
        }

        public static readonly BindableProperty CameraProperty = BindableProperty.Create("Camera",
           typeof(CameraOptions), typeof(PXC_Camera), CameraOptions.Rear);

        public CameraOptions Camera
        {
            get { return (CameraOptions)GetValue(CameraProperty); }
            set { SetValue(CameraProperty, value); }
        }

        public void SetPhotoResult(MediaFiles mediaFiles)
        {
            if (OnRecordOrPhotoResult != null)
            {
                OnRecordOrPhotoResult?.Invoke(new ResultEventArgs(mediaFiles));
            }
            if (OnRecordOrPhotoResultCommand != null && OnRecordOrPhotoResultCommand.CanExecute(mediaFiles))
            {
                OnRecordOrPhotoResultCommand.Execute(mediaFiles);
            }
        }

    }
}
