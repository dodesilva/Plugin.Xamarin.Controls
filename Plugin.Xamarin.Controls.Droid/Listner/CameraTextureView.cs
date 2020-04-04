using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Plugin.Xamarin.Controls.Droid.Listner
{
	public class CameraTextureView : TextureView
	{
		private int mRatioWidth = 0;
		private int mRatioHeight = 0;
		public CameraTextureView(Context context) : base(context, null)
		{
		}

		public CameraTextureView(Context context, IAttributeSet attrs) :
			base(context, attrs, 0)
		{
		}

		public CameraTextureView(Context context, IAttributeSet attrs, int defStyle) :
			base(context, attrs, defStyle)
		{
		}

		/// <summary>
		/// Sets the aspect ratio for this view. The size of the view will be measured based on the ratio
		/// calculated from the parameters. Note that the actual sizes of parameters don't matter, that
		/// is, calling setAspectRatio(2, 3) and setAspectRatio(4, 6) make the same result.
		/// </summary>
		/// <param name="width">Relative horizontal size.</param>
		/// <param name="height">Relative vertical size.</param>
		public void SetAspectRatio(int width, int height)
		{
			if (width == 0 || height == 0)
				throw new ArgumentException("Size cannot be negative.");
			mRatioWidth = width;
			mRatioHeight = height;
			RequestLayout();
		}

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
			int width = MeasureSpec.GetSize(widthMeasureSpec);
			int height = MeasureSpec.GetSize(heightMeasureSpec);
			if (0 == mRatioWidth || 0 == mRatioHeight)
			{
				SetMeasuredDimension(width, height);
			}
			else
			{
				if (width < (float)height * mRatioWidth / (float)mRatioHeight)
				{
					//SetMeasuredDimension(width, height * mRatioWidth / mRatioHeight);
					SetMeasuredDimension(width * mRatioHeight / mRatioWidth, height);
				}
				else
				{
					SetMeasuredDimension(height, width);
				}
			}
		}
	}
}