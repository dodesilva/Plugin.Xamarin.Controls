using AVFoundation;
using UIKit;

namespace Plugin.Xamarin.Controls.IOS.Classes
{
    public class VideoSurface : UIView
    {

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            if (Layer.Sublayers == null || Layer.Sublayers.Length == 0)
                return;
            foreach (var layer in Layer.Sublayers)
            {
                var avPlayerLayer = layer as AVPlayerLayer;
                if (avPlayerLayer != null)
                    avPlayerLayer.Frame = Bounds;
            }
        }

        #region IDisposable
        public bool IsDisposed { get; private set; }

        protected override void Dispose(bool disposing)
        {
            IsDisposed = true;

            base.Dispose(disposing);
        }
        #endregion
    }
}