using Plugin.Xamarin.Controls;
using Plugin.Xamarin.Controls.EnumFiles;
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
    public partial class MultySelectWithButtonPage : ContentPage
    {
        public MultySelectWithButtonPage()
        {
            InitializeComponent();
            BindingContext = new MultiSendViewModel();
        }

        
    }
}