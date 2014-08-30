using System;
using System.Collections.Generic;
using System.Text;
using Barter.li.Win.BL.Network;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Barter.li.Win.Model.SearchResponseJsonTypes;
using Barter.li.Win.BL.APIServices;

namespace Barter.li.Win.BL.DataHandler
{
    //Make request to APISevice
    public class SearchDatahandler
    {
        CancellationToken token;
        public SearchDatahandler()
        { }

        public  Task<SearchResponse> LoadDataAsync(double latitude, double longitude, int pageNo)
        {
            token = new CancellationToken(false);
            NetworkContext networkContext = new NetworkContext();
            networkContext.cancellationToken = token;
            networkContext.httpMethod = HttpMethod.Get;
            networkContext.retryCount = 3;
            networkContext.isSecureConnection = false;
            string baseUrl = "search.json?";
            baseUrl += "per=10 &page=" + pageNo + "&longitude=" + longitude + "&latitude=" + latitude;
            networkContext.URL = baseUrl;
            BliAPIService a = new BliAPIService();
            return  a.SendRequestAsync<SearchResponse>(networkContext);
        }


        public async void LoadData(Action<bool, SearchResponse> onDataReceivedSuccessfully, Action<Exception> onError, double latitude, double longitude, int pageNo)
        {
            if (onDataReceivedSuccessfully == null || onError == null)
            {
                throw new ArgumentNullException("onDataReceivedSuccessfully or onError is Null : Argument can not be null");
            }

            try
            {
                SearchResponse response = await LoadDataAsync(latitude, longitude, pageNo);
                if (response != null)
                {
                    onDataReceivedSuccessfully(false, response);
                }
                else
                {
                    onError(new Exception("Empty Response received"));
                }
            }
            catch (ArgumentException ex)
            {
                onError(ex);
            }
            catch (Exception e)
            {
                onError(e);
            }
        }
    }
}
