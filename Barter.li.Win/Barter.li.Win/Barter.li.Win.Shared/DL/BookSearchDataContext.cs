using Barter.li.Win.BL.DataHandler;
using Barter.li.Win.Model.SearchResponseJsonTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Barter.li.Win.DL
{
    public class BookSearchDataContext
    {
        SearchDatahandler searchDH;
        private List<Search> books;

        public bool hasMore
        {
            get;
            private set;
        }

        public BookSearchDataContext()
        {
            searchDH = new SearchDatahandler();
            books = new List<Search>();
            hasMore = true;
        }


        public async Task<SearchResponse> SearchBooks(double latitude, double longitude, int pageNo, int startIndex = 0)
        {
            int count = (pageNo + 1) * 10;
            if (books.Count > 0 && books.Count > count)
            {
                return await SearchRequestInMemory(latitude, longitude, pageNo, startIndex);
            }
            else
            {
                return await SearchRequestOnNetwork(latitude, longitude, pageNo);
            }               
        }

        public int FindPageNoFromSelectedIndex(int SelectedIndex)
        {
            if (SelectedIndex == 0)
            {
                return 0;
            }
            else
            {
                return (SelectedIndex / 10);
            }
        }

        private async Task<SearchResponse> SearchRequestOnNetwork(double latitude, double longitude, int pageNo)
        {
            try
            {
                SearchResponse response = await searchDH.LoadDataAsync(latitude, longitude, pageNo);
                HasMoreBooks(response.Search);
                await Task.Run(() => StoreSearchResults(response));
                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<SearchResponse> SearchRequestInMemory(double latitude, double longitude, int pageNo, int startIndex)
        {
            try
            {
                SearchResponse response = new SearchResponse();
                Search[] searchResult;
                searchResult = books.ToArray();
                response.Search = searchResult;
                return response;
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        private void StoreSearchResults(SearchResponse searchResponse)
        {
            if (searchResponse != null && searchResponse.Search != null && searchResponse.Search.Length > 0)
            {
                foreach (var item in searchResponse.Search)
                {

                    try
                    {
                        books.Add(item);
                    }
                    catch
                    {
                        System.Diagnostics.Debug.WriteLine("Failed to add book in Memory");
                    }

                }
            }
        }

        private void HasMoreBooks(Search[] search)
        {
            if (search != null && search.Length == 10)
            {
                hasMore = true;
            }
            else
            {
                hasMore = false;
            }
        }
    }
}
