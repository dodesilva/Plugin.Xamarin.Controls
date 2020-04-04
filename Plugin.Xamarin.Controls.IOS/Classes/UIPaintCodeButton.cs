using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Plugin.Xamarin.Controls.IOS.Classes
{
    public class UIPaintCodeButton : UIButton
    {
        Action<CGRect> _drawing;
        public UIPaintCodeButton(Action<CGRect> drawing)
        {
            _drawing = drawing;
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            _drawing(rect);
        }

    }
}