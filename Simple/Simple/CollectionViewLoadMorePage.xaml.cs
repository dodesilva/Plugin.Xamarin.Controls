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
    public partial class CollectionViewLoadMorePage : ContentPage
    {
        public CollectionViewLoadMorePage()
        {
            InitializeComponent();
            BindingContext = new CollectionViewLoadMoreViewModel();
        }

        private void CollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            scrl.Text = string.Format("First {0} : Last {1} : Center {2}", e.FirstVisibleItemIndex, e.LastVisibleItemIndex, e.CenterItemIndex);
        }
    }
}