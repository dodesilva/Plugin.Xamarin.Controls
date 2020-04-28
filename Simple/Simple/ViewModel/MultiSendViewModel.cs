using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Xamarin.Controls.MultySelectable;
using Simple.Models;
using Xamarin.Forms;

namespace Simple.ViewModel
{
    public class MultiSendViewModel:BaseViewModel
    {
        private bool _isRefreshing;
        private bool _isBusy;
        public int PageSize = 10;
        public int pageinsert = 1;
        private int RefreshDuration = 1;
        List<string> _listItems;

        private ObservableCollection<SelectableData<MultiModel>> _getUserList;
        public ObservableCollection<SelectableData<MultiModel>> GetUserList
        {
            get { return _getUserList; }
            set { SetValue(ref _getUserList, value); }
        }

        public List<string> ListItems
        {
            get { return _listItems; }
            set { SetValue(ref _listItems, value); }
        }
        public List<SelectableData<MultiModel>> MultiModels;

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

        public List<MultiModel> Roomchecked { get; set; }
        #region Singleton
        private static MultiSendViewModel instance;

        public static MultiSendViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MultiSendViewModel();
            }

            return instance;
        }
        #endregion
        public MultiSendViewModel()
        {
            instance = this;
            Roomchecked = new List<MultiModel>();
            GetUserList = new ObservableCollection<SelectableData<MultiModel>>();
            MultiModels = new List<SelectableData<MultiModel>>();
            LoadValues();
        }
        public List<string> GetListString()
        {
            ListItems = new List<string>();
            ListItems.Add("Sair do grupo");
            ListItems.Add("This second item");
            return ListItems;
        }
        private void LoadValues()
        {
            GetUserList.Clear();
            ListItems = new List<string>();
            ListItems.Add("Sair do grupo");
            ListItems.Add("This second item");
            for (int i = 0; i < 100; i++)
            {
                var data = new MultiModel
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
                GetUserList = new ObservableCollection<SelectableData<MultiModel>>(MultiModels);
            }
            else
            {
                GetUserList = new ObservableCollection<SelectableData<MultiModel>>(MultiModels.Where(u=>(u.Data as MultiModel).Name.ToLower().Contains(Filter.ToLower())));
            }
        }

        public ICommand ShareCommand
        {
            get
            {
                return new Command<SelectableData<MultiModel>>(share);
            }
        }
        public ICommand PopupCommand
        {
            get
            {
                return new Command(Popup);
            }
        }

        private void Popup(object obj)
        {
           
        }

        public ICommand SearchCommand
        {
            get
            {
                return new Command(Search);
            }
        }

        private void share(SelectableData<MultiModel> obj)
        {
            if (obj != null)
            {
                if (!Roomchecked.Any(u => u.Name == (obj.Data as MultiModel).Name) && obj.IsSelected)
                {
                    Roomchecked.Add(new MultiModel
                    {
                        Image = (obj.Data as MultiModel).Image,
                        Name = (obj.Data as MultiModel).Name
                    });
                    App.Current.MainPage.DisplayAlert("Share", "Shared with :" + (obj.Data as MultiModel).Name + " Total: "+Roomchecked.Count, "OK");
                }
                else if(obj.IsSelected==false&& Roomchecked.Any(u => u.Name == (obj.Data as MultiModel).Name))
                {
                    var usid = Roomchecked.FirstOrDefault(u => u.Name == (obj.Data as MultiModel).Name);
                    Roomchecked.Remove(usid);
                    App.Current.MainPage.DisplayAlert("Share", "Removed :" + (obj.Data as MultiModel).Name + " Total: " + Roomchecked.Count, "OK");
                }

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
    }
}
