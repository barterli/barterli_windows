using Barter.Li.Win.DL;
using Barter.Li.Win.Model.SearchResponseJsonTypes;
using Barter.Li.Win.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Barter.li.Win.Shared.ViewModel
{
    class MainPageViewModel: BaseViewModel
    {
        public ObservableCollection<Search> BooksSearchResponseCollection { get; set; }
        private BookSearchDataContext bookSearchDC { get; set; }

        public MainPageViewModel()
        {
            BooksSearchResponseCollection = new ObservableCollection<Search>();
            bookSearchDC = new BookSearchDataContext();
            BeginInitialLoad();
        }
        private async void BeginInitialLoad()
        {
            if(await UpdateLocation())
            {
                try
                {
                    var response = await Task.Run(() => bookSearchDC.SearchBooks(latestPosition.Position.Latitude, latestPosition.Position.Longitude, 1));
                    foreach (var item in response.Search)
                        BooksSearchResponseCollection.Add(item);
                }
                catch
                {

                }
            }
        }
    }
}
