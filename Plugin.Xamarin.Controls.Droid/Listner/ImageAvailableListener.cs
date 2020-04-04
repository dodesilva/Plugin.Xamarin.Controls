using Android.App;
using Android.Media;
using Java.IO;
using Java.Lang;
using Java.Nio;
using System;

namespace Plugin.Xamarin.Controls.Droid.Listner
{
    public class ImageAvailableListener : Java.Lang.Object, ImageReader.IOnImageAvailableListener
    {
        public event EventHandler<byte[]> Photo;
        public void OnImageAvailable(ImageReader reader)
        {
            Image image = null;

            try
            {
                image = reader.AcquireLatestImage();
                var buffer = image.GetPlanes()[0].Buffer;
                var imageData = new byte[buffer.Capacity()];
                buffer.Get(imageData);

                Photo?.Invoke(this, imageData);
            }
            catch (System.Exception)
            {
                // ignored
            }
            finally
            {
                image?.Close();
            }
        }
    }
}