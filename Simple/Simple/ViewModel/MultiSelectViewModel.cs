using Plugin.Xamarin.Controls.Helpers;
using Plugin.Xamarin.Controls.MultySelectable;
using Simple.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Simple.ViewModel
{
    public class MultiSelectViewModel:BaseViewModel
    {
        private bool _isRefreshing;
        private bool _isBusy;
        public int PageSize = 10;
        public int pageinsert = 1;
        private int RefreshDuration = 1;
        private ObservableCollection<SelectableData<MultiModel>> _getUserList;
        public ObservableCollection<SelectableData<MultiModel>> GetUserList
        {
            get { return _getUserList; }
            set { SetValue(ref _getUserList, value); }
        }

        public List<SelectableData<MultiModel>> MultiModels;
        public List<string> ListGroup;

        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set { SetValue(ref _filter, value); }
        }
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetValue(ref _isRefreshing, value); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetValue(ref _isBusy, value); }
        }

        public MultiSelectViewModel()
        {
            MultiModels = new List<SelectableData<MultiModel>>();
            GetUserList = new ObservableCollection<SelectableData<MultiModel>>();
            ListGroup = new List<string>();
            LoadValues();
        }

        private void LoadValues()
        {
            GetUserList.Clear();
            for (int i = 0; i < 100; i++)
            {
                var data=new MultiModel
                {
                    Image = "ImageTest.jpg",
                    Name = "Test" + i
                };

                MultiModels.Add(new SelectableData<MultiModel>
                {
                    Data = data,
                    IsSelected = false
                });
            }
            GetLoadToList(MultiModels.Skip(0).Take(PageSize).ToList());
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(Filter))
            {
                LoadValues();
            }
            else
            {
                GetUserList = new ObservableCollection<SelectableData<MultiModel>>(MultiModels.Where(u => (u.Data as MultiModel).Name.ToLower().Contains(Filter.ToLower())));
            }
        }

        public ICommand ShareCommand
        {
            get
            {
                return new Command<SelectableData<MultiModel>>(share);
            }
        }


        public ICommand SearchCommand
        {
            get
            {
                return new Command(Search);
            }
        }
        public ICommand LoadMoreCommand
        {
            get
            {
                return new Command(() => {
                    TakeMore();
                });
            }
        }

        private async void TakeMore()
        {
            IsBusy = true;
            int TotalCount = MultiModels.Count;
            int pagerCount = GetUserList.Count;
            int pg = PageSize * pageinsert;
            var page = (TotalCount / PageSize) - (TotalCount % PageSize == 0 ? 1 : 0);
            
            if (pageinsert == 1 || pageinsert < page + 1)
            {
                await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
                GetLoadToList(MultiModels.Skip(pg).Take(PageSize).ToList());
                pageinsert++;

            }
            IsBusy = false;
        }
        int id = 0;
        private void GetLoadToList(List<SelectableData<MultiModel>> list)
        {
            foreach (var l in list)
            {
                GetUserList.Add(l);
            }
        }
        private void share(SelectableData<MultiModel> obj)
        {
            if (obj != null)
            {
                if (!ListGroup.Any(u => u == (obj.Data as MultiModel).Name)&& obj.IsSelected)
                {
                    ListGroup.Add((obj.Data as MultiModel).Name);
                    App.Current.MainPage.DisplayAlert("Group", "Group Count: " + ListGroup.Count + " / " + (obj.Data as MultiModel).Name + " is added to group", "OK");
                }
                else if (ListGroup.Any(u => u == (obj.Data as MultiModel).Name)&& !obj.IsSelected)
                {
                    var name = ListGroup.FirstOrDefault(u => u == (obj.Data as MultiModel).Name);
                    ListGroup.Remove(name);
                    App.Current.MainPage.DisplayAlert("Group", "Group Count: " + ListGroup.Count + " / " + (obj.Data as MultiModel).Name + " is removed from group", "OK");
                }
            }
        }
    }
}
