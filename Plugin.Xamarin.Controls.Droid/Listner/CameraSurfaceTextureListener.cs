using Android.Graphics;
using Android.Views;
using Plugin.Xamarin.Controls.Droid.Classes;
using Plugin.Xamarin.Controls.Droid.Controls;

namespace Plugin.Xamarin.Controls.Droid.Listner
{
    public class CameraSurfaceTextureListener : Java.Lang.Object, TextureView.ISurfaceTextureListener
    {
        private readonly CameraDroidView owner;
        public CameraSurfaceTextureListener(CameraDroidView owner)
        {
            if (owner == null)
                throw new System.ArgumentNullException("owner");
            this.owner = owner;
        }

        public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
        {
            owner.StartBackgroundThread();
            owner.OpenCamera(width, height);
        }

        public bool OnSurfaceTextureDestroyed(SurfaceTexture surface)
        {
            owner.StopBackgroundThread();
            return true;
        }

        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
            owner.ConfigureTransform(width, height);
        }

        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {
           
        }
    }
}