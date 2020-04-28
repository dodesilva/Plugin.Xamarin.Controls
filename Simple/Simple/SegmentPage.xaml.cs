using Plugin.Xamarin.Controls.EventArgsFile;
using Plugin.Xamarin.Controls.Helpers;
using Simple.ContentViews;
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
    public partial class SegmentPage : ContentPage
    {
        public SegmentPage()
        {
            InitializeComponent();
            List<BarIconAndTitle> barTexts = new List<BarIconAndTitle>();
            barTexts.Add(new BarIconAndTitle { IconText = "md-person", Title = "Users" });
            barTexts.Add(new BarIconAndTitle { IconText = "md-group", Title = "Group" });
            segment.Children = barTexts;
        }

        private void PXC_SegmenteBar_SelectedBarItemChanged(object sender, SelectedBarItemChangedEventArgs e)
        {
           
        }

        private void segment_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem== "Users")
            {

            }
        }
    }
}