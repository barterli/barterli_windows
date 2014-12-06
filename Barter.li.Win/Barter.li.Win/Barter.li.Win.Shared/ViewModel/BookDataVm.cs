using Barter.Li.Win.Model.SearchResponseJsonTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Barter.Li.Win.DL;
using Windows.UI.Xaml.Data;
using Barter.Li.Win.Util;
using System.Threading;

namespace Barter.Li.Win.ViewModel
{
    public class BookSummaryInfo
    {
        Search _bookData;
        public BookSummaryInfo(Search bookData)
        {
            _bookData = bookData;
        }
        public int Id
        {
            get
            {
                return _bookData.Id;
            }
        }

        public string Title { get { return _bookData.Title; } }

        public string Author { get { return _bookData.Author; } }

        public string ImageUrl
        {
            get
            {
                if (!_bookData.ImagePresent)
                {
                    return "Assets/barterli_hd.png";
                }

                else
                {
                    return _bookData.ImageUrl;
                }
            }
        }

        public string[] Tags { get { return _bookData.Tags; } }

        public string OwnerImageUrl { get { return _bookData.OwnerImageUrl; } }

        public bool? ImagePresent { get { return _bookData.ImagePresent; } }
    }

    public class BookCollection : IncrementalLoadingCollection<BookSummaryInfo>
    {
        private BookSearchDataContext _bookDataContext;
        private int _currentpage;
        public event DataLoadingStatusChangedEventHandler DataLoadingStatusChanged;
        public delegate void DataLoadingStatusChangedEventHandler(bool isLoading);

        public BookCollection(BookSearchDataContext bookDataContext)
        {
            this._bookDataContext = bookDataContext;
            _currentpage = 0;
        }

        public bool IsBusy
        {
            get;
            private set;
        }

        public void RaiseDataLoadingStatusChanged(bool isLoading)
        {
            if (DataLoadingStatusChanged != null)
            {
                DataLoadingStatusChanged(isLoading);
            }
        }             

        protected async override Task<ObservableCollection<BookSummaryInfo>> LoadNextItemsAsync(int count)
        {
            RaiseDataLoadingStatusChanged(true);
            if (_bookDataContext != null)
            {
                SearchResponse response = await _bookDataContext.SearchBooks(12.9399408, 77.6276092, _currentpage);
                _currentpage++;
                if (response != null && response.Search != null && response.Search.Length > 0)
                {
                    List<BookSummaryInfo> bookSummaryInfo = new List<BookSummaryInfo>();
                    foreach (var bookinfo in response.Search)
                    {
                        bookSummaryInfo.Add(new BookSummaryInfo(bookinfo));
                    }
                    await Task.Delay(10);
                    RaiseDataLoadingStatusChanged(false);
                    return new ObservableCollection<BookSummaryInfo>(bookSummaryInfo);
                }
            }
            RaiseDataLoadingStatusChanged(false);
            return null;
        }

        protected override bool CanLoadMoreItems()
        {
            return _bookDataContext.hasMore;
        }
    }

    public class BookDataVM : BaseViewModel
    {
        BookSearchDataContext data;
        bool _IsBusy;
        public BookDataVM()
        {
            data = new BookSearchDataContext();
            BookCollection = new BookCollection(data);
            BookCollection.DataLoadingStatusChanged += BookCollection_DataLoadingStatusChanged;
        }

        public BookDataVM(BookSearchDataContext data)
        {
            this.data = data;
            BookCollection = new BookCollection(data);
            BookCollection.DataLoadingStatusChanged += BookCollection_DataLoadingStatusChanged;
        }

        void BookCollection_DataLoadingStatusChanged(bool isLoading)
        {
            IsBusy = isLoading;
        }

        private BookCollection _BookCollection;

        public BookCollection BookCollection
        {
            get
            {
                return _BookCollection;
            }
            private set
            {
                if (value != _BookCollection)
                {
                    _BookCollection = value;
                    NotifyPropertyChanged("BookCollection");
                }
            }
        }

        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            private set
            {
                if (value != _IsBusy)
                {
                    _IsBusy = value;
                    NotifyPropertyChanged("IsBusy");
                }
            }
        }      

        public BookSearchDataContext BookDataContext
        {
            get
            {
                return data;
            }
        }



    }
}
