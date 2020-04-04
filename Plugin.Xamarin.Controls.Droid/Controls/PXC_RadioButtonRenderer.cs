using System;
using System.ComponentModel;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PXC_RadioButton), typeof(PXC_RadioButtonRenderer))]
namespace Plugin.Xamarin.Controls.Droid.Controls
{
    public class PXC_RadioButtonRenderer : ViewRenderer<PXC_RadioButton, RadioButton>
    {
        private ColorStateList _defaultTextColor;
        Context _context;

        public PXC_RadioButtonRenderer(Context context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// Called when [element changed].
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<PXC_RadioButton> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                if ((int)Build.VERSION.SdkInt < 21)
                {
                    SetLayerType(LayerType.Software, null);
                }
            }

            if (Control == null)
            {
                var radButton = new RadioButton(Context);
                _defaultTextColor = radButton.TextColors;

                radButton.CheckedChange += radButton_CheckedChange;

                SetNativeControl(radButton);
            }
            Control.Text = e.NewElement.Text;
            Control.Checked = e.NewElement.Checked;
            UpdateTextColor();
            UpdateButtonTinColor();

            if (e.NewElement.FontSize > 0)
            {
                Control.TextSize = (float)e.NewElement.FontSize;
            }
            if (!string.IsNullOrEmpty(e.NewElement.FontName.ToString()))
            {
                Control.Typeface = TrySetFont(e.NewElement.FontName.ToString());
            }
        }

        private void UpdateButtonTinColor()
        {
            var stateList = new ColorStateList(
                new int[][] {
                    new int[] { Android.Resource.Attribute.StateChecked
                },
                new int[] { Android.Resource.Attribute.StateEnabled
                }
                },
                new int[] {
                    Element.CheckedColor.ToAndroid(),
                    Element.UnCheckedColor.ToAndroid()
                });
            Control.ButtonTintList = stateList;
        }

        /// <summary>
        /// Handles the CheckedChange event of the radButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CompoundButton.CheckedChangeEventArgs"/> instance containing the event data.</param>
        private void radButton_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Element.Checked = e.IsChecked;
        }

        /// <summary>
        /// Handles the <see cref="E:ElementPropertyChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case "Checked":
                    Control.Checked = Element.Checked;
                    break;
                case "Text":
                    Control.Text = Element.Text;
                    break;
                case "TextColor":
                    UpdateTextColor();
                    break;
                case "FontName":
                    if (!string.IsNullOrEmpty(Element.FontName.ToString()))
                    {
                        Control.Typeface = TrySetFont(Element.FontName.ToString());
                    }
                    break;

                case "FontSize":
                    if (Element.FontSize > 0)
                    {
                        Control.TextSize = (float)Element.FontSize;                        
                    }
                    break;

            }
        }

        /// <summary>
        ///     Tries the set font.
        /// </summary>
        /// <param name="fontName">Name of the font.</param>
        /// <returns>Typeface.</returns>
        private Typeface TrySetFont(string fontName)
        {
            string ext = string.Empty;
            try
            {
                if (fontName == "OpenSans_Bold" || fontName == "FFF_Tusj" || fontName == "OpenSans_Regular" || fontName == "architep")
                {
                    ext = ".ttf";
                }
                else
                {
                    ext = ".otf";
                }
                return Typeface.CreateFromAsset(Context.Assets, fontName + ext);
            }
            catch (Exception ex)
            {
                Console.WriteLine("not found in assets. Exception: {0}", ex);
                try
                {
                    return Typeface.CreateFromFile(fontName);
                }
                catch (Exception ex1)
                {
                    Console.WriteLine("not found by file. Exception: {0}", ex1);

                    return Typeface.Default;
                }
            }
        }

        /// <summary>
        /// Updates the color of the text
        /// </summary>
        private void UpdateTextColor()
        {
            if (Control == null || Element == null)
                return;

            if (Element.TextColor == global::Xamarin.Forms.Color.Default)
                Control.SetTextColor(_defaultTextColor);
            else
                Control.SetTextColor(Element.TextColor.ToAndroid());
            
        }
    }
}