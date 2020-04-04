using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Plugin.Xamarin.Controls.MultySelectable;
using Simple.Models;
using Xamarin.Forms;

namespace Simple.ViewModel
{
    public class MultiSendViewModel:BaseViewModel
    {
        private MultiSelectObservableCollection<MultiModel> _getUserList;
        public MultiSelectObservableCollection<MultiModel> GetUserList
        {
            get { return _getUserList; }
            set { SetValue(ref _getUserList, value); }
        }

        public List<MultiModel> MultiModels;

        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set { SetValue(ref _filter, value); }
        }
        public MultiSendViewModel()
        {
            MultiModels = new List<MultiModel>();
            LoadValues();
        }

        private void LoadValues()
        {
            
           for(int i = 0; i < 10; i++)
            {
                MultiModels.Add(new MultiModel
                {
                    Image = "ImageTest.jpg",
                    Name = "Test" + i
                });
            }
            Search();
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(Filter))
            {
                GetUserList = new MultiSelectObservableCollection<MultiModel>(MultiModels);
            }
            else
            {
                GetUserList = new MultiSelectObservableCollection<MultiModel>(MultiModels.Where(u=>u.Name.ToLower().Contains(Filter.ToLower())));
            }
        }

        public ICommand ShareCommand
        {
            get
            {
                return new Command<MultiModel>(share);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new Command(Search);
            }
        }

        private void share(MultiModel obj)
        {
            App.Current.MainPage.DisplayAlert("Share", "Shared with :" + obj.Name, "OK");
        }
    }
}
