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
    public class CollectionViewLoadMoreViewModel:BaseViewModel
    {
        private bool _isRefreshing;
        private bool _isBusy;
        public int PageSize = 10;
        public int pageinsert = 1;
        private int RefreshDuration = 3;
        private List<SelectableData<MultiModel>> GetStatusUserList;

        private ObservableCollection<SelectableData<MultiModel>> _multiModels;
        public ObservableCollection<SelectableData<MultiModel>> GetUserList
        {
            get { return _multiModels; }
            set { SetValue(ref _multiModels, value); }
        }

        public CollectionViewLoadMoreViewModel()
        {
            GetUserList = new ObservableCollection<SelectableData<MultiModel>>();
            GetStatusUserList = new List<SelectableData<MultiModel>>();
            LoadList();
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
        private void LoadList()
        {
            GetUserList.Clear();
            GetStatusUserList.Clear();
            IsRefreshing = true;
            for (int i = 0; i < 100; i++)
            {
                var data=new MultiModel
                {
                    Image = "ImageTest.jpg",
                    Name = "Items : "+i
                };
                GetStatusUserList.Add(new SelectableData<MultiModel>
                {
                    Data = data,
                    IsSelected = false
                }) ;
            }

            // GetStatusUserList.Reverse();
            GetLoadToList(GetStatusUserList.Skip(0).Take(PageSize).ToList());
            IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    LoadList();
                });
            }
        }

        public ICommand SelecItemCommand
        {
            get
            {
                return new Command<MultiModel>((e) =>
                {
                    LoadListtitle(e);
                });
            }
        }

        private void LoadListtitle(MultiModel e)
        {
            
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
            int TotalCount = GetStatusUserList.Count;
            int pagerCount = GetUserList.Count;
            int pg = PageSize * pageinsert;
            var page = (TotalCount / PageSize) - (TotalCount % PageSize == 0 ? 1 : 0);
            PageSize += 10;
            if (pageinsert == 1 || pageinsert < page + 1)
            {
                await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
                GetLoadToList(GetStatusUserList.Skip(pg).Take(PageSize).ToList());
                pageinsert++;
               
            } IsBusy = false;
        }
        
        private void GetLoadToList(List<SelectableData<MultiModel>> list)
        {
           foreach(var l in list)
            {
                GetUserList.Add(l);
            }
        }
    }
}
