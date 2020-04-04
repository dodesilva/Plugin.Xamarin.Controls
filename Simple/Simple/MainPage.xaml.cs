using Simple.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Simple
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Dictionary<int, string> ListGenero = new Dictionary<int, string>();
            ListGenero.Add(0, "Masculino");
            ListGenero.Add(1, "Femenino");
            pxcradio.ItemsSource = ListGenero.Values;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var pag = new PageVideo();
            
            Navigation.PushModalAsync(pag);
        }

        private void ButtonMultiselect_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MultySelectWithCheckboxPage());
        }
        private void ButtonMultiSend_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MultySelectWithButtonPage());
        }

        private void ButtonMultiselectswitch_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MultySelectWithSwitchPage());
        }

        private void Buttoncam_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CameraPage());
        }
    }
}
