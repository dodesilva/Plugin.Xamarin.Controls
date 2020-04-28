using Plugin.Xamarin.Controls;
using Simple.ViewModel;
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
    public partial class ButtonAndImagePage : ContentPage
    {
        public ButtonAndImagePage()
        {
            InitializeComponent();
            BindingContext = new MultiSendViewModel();
        }

    }
}