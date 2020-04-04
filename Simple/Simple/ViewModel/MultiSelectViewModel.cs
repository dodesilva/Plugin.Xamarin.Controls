using Plugin.Xamarin.Controls.MultySelectable;
using Simple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Simple.ViewModel
{
    public class MultiSelectViewModel:BaseViewModel
    {
        private MultiSelectObservableCollection<MultiModel> _getUserList;
        public MultiSelectObservableCollection<MultiModel> GetUserList
        {
            get { return _getUserList; }
            set { SetValue(ref _getUserList, value); }
        }

        public List<MultiModel> MultiModels;
        public List<string> ListGroup;

        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set { SetValue(ref _filter, value); }
        }

        public MultiSelectViewModel()
        {
            MultiModels = new List<MultiModel>();
            ListGroup = new List<string>();
            LoadValues();
        }

        private void LoadValues()
        {

            for (int i = 0; i < 10; i++)
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
                GetUserList = new MultiSelectObservableCollection<MultiModel>(MultiModels.Where(u => u.Name.ToLower().Contains(Filter.ToLower())));
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
            if (!ListGroup.Any(u => u == obj.Name))
            {
                ListGroup.Add(obj.Name);
                App.Current.MainPage.DisplayAlert("Group","Group Count: "+ListGroup.Count+" / "+ obj.Name+" is added to group" , "OK");
            }
            else
            {
                var name = ListGroup.FirstOrDefault(u => u == obj.Name);
                ListGroup.Remove(name);
                App.Current.MainPage.DisplayAlert("Group", "Group Count: " + ListGroup.Count + " / " + obj.Name + " is removed from group", "OK");
            }
            
        }
    }
}
