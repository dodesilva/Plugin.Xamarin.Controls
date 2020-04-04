using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Plugin.Xamarin.Controls.Droid.Classes
{
    public class AndroidImageHelper
    {
        private static IImageSourceHandler GetHandler(ImageSource source)
        {
            IImageSourceHandler returnValue = null;
            if (source is UriImageSource)
            {
                returnValue = new ImageLoaderSourceHandler();
            }
            else if (source is FileImageSource)
            {
                returnValue = new FileImageSourceHandler();
            }
            else if (source is StreamImageSource)
            {
                returnValue = new StreamImagesourceHandler();
            }
            return returnValue;
        }

        /// <summary>
        /// For converting Xamarin Forms ImageSource object to Native Image type
        /// </summary>
        /// <param name="source"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<Bitmap> GetBitmapFromImageSourceAsync(ImageSource source, Context context)
        {
            var handler = GetHandler(source);
            var returnValue = (Bitmap)null;

            returnValue = await handler.LoadImageAsync(source, context);

            return returnValue;
        }

        public static string SaveFile(byte[] imageByte, string folder = null, string subfolder = null)
        {

            Java.IO.File mFile = null;
            if (string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(subfolder))
            {
                if (string.IsNullOrEmpty(folder))
                    mFile = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), subfolder);
                else if (string.IsNullOrEmpty(subfolder))
                    mFile = new Java.IO.File(folder, "Image");
                else if (string.IsNullOrEmpty(folder) && string.IsNullOrEmpty(subfolder))
                    mFile = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "Image");
            }
            else
            {
                mFile = new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory + "/" + folder, subfolder);
            }
            if (!mFile.Exists())
            {
                mFile.Mkdir();
            }
            var guid = Guid.NewGuid();
            var file = System.IO.Path.Combine(mFile.AbsolutePath, guid + ".jpeg");
            System.IO.File.WriteAllBytes(file, imageByte);

            return file;
        }

        public static async Task<byte[]> RotateImage(string path)
        {
            byte[] imageBytes;

            var originalImage = BitmapFactory.DecodeFile(path);
            var rotation = GetRotation(path);
            var width = (originalImage.Width * 0.25);
            var height = (originalImage.Height * 0.25);
            var scaledImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, true);

            Bitmap rotatedImage = scaledImage;
            if (rotation != 0)
            {
                var matrix = new Matrix();
                matrix.PostRotate(rotation);
                rotatedImage = Bitmap.CreateBitmap(scaledImage, 0, 0, scaledImage.Width, scaledImage.Height, matrix, true);
                scaledImage.Recycle();
                scaledImage.Dispose();
            }

            using (var ms = new MemoryStream())
            {
               await rotatedImage.CompressAsync(Bitmap.CompressFormat.Jpeg, 90, ms);
                imageBytes = ms.ToArray();
            }

            originalImage.Recycle();
            rotatedImage.Recycle();
            originalImage.Dispose();
            rotatedImage.Dispose();
            GC.Collect();

            return imageBytes;
        }

        public static async Task<ImageSource> RotateImage(byte[] buffer)
        {
            var rotation = GetRotation(buffer);
            var originalImage = BitmapFactory.DecodeByteArray(buffer, 0, buffer.Length);
            var width = (originalImage.Width * 0.25);
            var height = (originalImage.Height * 0.25);
            var scaledImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, true);

            Bitmap rotatedImage = scaledImage;
            if (rotation != 0)
            {
                var matrix = new Matrix();
                matrix.PostRotate(rotation);
                rotatedImage = Bitmap.CreateBitmap(scaledImage, 0, 0, scaledImage.Width, scaledImage.Height, matrix, true);
                scaledImage.Recycle();
                scaledImage.Dispose();
            }

            var stream = new MemoryStream();
            await rotatedImage.CompressAsync(Bitmap.CompressFormat.Jpeg, 90, stream);
            stream.Seek(0L, SeekOrigin.Begin);

            return ImageSource.FromStream(() => stream);
        }
        private static int GetRotation(byte[]buffer)
        {
            var filestream = new MemoryStream(buffer);
            using (var ei = new ExifInterface(filestream))
            {
                var orientation = (Android.Media.Orientation)ei.GetAttributeInt(ExifInterface.TagOrientation, (int)Android.Media.Orientation.Normal);

                switch (orientation)
                {
                    case Android.Media.Orientation.Rotate90:
                        return 90;
                    case Android.Media.Orientation.Rotate180:
                        return 180;
                    case Android.Media.Orientation.Rotate270:
                        return 270;
                    default:
                        return 0;
                }
            }
        }

        private static int GetRotation(string filePath)
        {
            using (var ei = new ExifInterface(filePath))
            {
                var orientation = (Android.Media.Orientation)ei.GetAttributeInt(ExifInterface.TagOrientation, (int)Android.Media.Orientation.Normal);

                switch (orientation)
                {
                    case Android.Media.Orientation.Rotate90:
                        return 90;
                    case Android.Media.Orientation.Rotate180:
                        return 180;
                    case Android.Media.Orientation.Rotate270:
                        return 270;
                    default:
                        return 0;
                }
            }
        }
    }
}