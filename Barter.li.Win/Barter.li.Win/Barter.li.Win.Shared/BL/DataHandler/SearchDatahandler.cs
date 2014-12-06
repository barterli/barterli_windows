﻿using System;
using System.Collections.Generic;
using System.Text;
using Barter.Li.Win.BL.Network;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Barter.Li.Win.Model.SearchResponseJsonTypes;
using Barter.Li.Win.BL.APIServices;
using Barter.Li.Win.BarterliException;
using Barter.li.Win.BL.Netowork;

namespace Barter.Li.Win.BL.DataHandler
{    
    /// <summary>
    /// Make request to APISevice 
    /// Handles the Data and give it to ViewModel
    /// </summary>
    public class SearchDatahandler
    {
        CancellationToken token;
        public SearchDatahandler()
        { }

        public async Task<SearchResponse> LoadDataAsync(double latitude, double longitude, int pageNo, string searchQuery = "")
        {
            token = new CancellationToken(false);
            NetworkContext networkContext = new NetworkContext();
            networkContext.CancellationToken = token;
            networkContext.HttpMethod = HttpMethod.Get;
            networkContext.RetryCount = 3;
            networkContext.RequestId = 101;
            networkContext.IsSecureConnection = false;
            string baseUrl = "search.json?";            
            baseUrl += "per=10 &page=" + pageNo + "&longitude=" + longitude + "&latitude=" + latitude;
            if(!string.IsNullOrWhiteSpace(searchQuery))
            {
                baseUrl += "&search=" + searchQuery;
            }

            networkContext.URL = baseUrl;
            APIService a = new APIService();
            try
            {
                return await Task.Run(() => a.SendRequestAsync<SearchResponse>(networkContext).value);
            }
            catch (ArgumentNullException e)
            {
                //send bug report
                throw e;
            }
            catch (BarterLiApiRequestException e)
            {
                //send bug report
                throw e;
            }
            catch (Exception e)
            {
                //send bug report
                throw e;
            }
        }

        public async Task<SearchResponse> SearchBook(double latitude, double longitude, int pageNo, string searchQuery)
        {
            return await LoadDataAsync(latitude, longitude, pageNo, searchQuery);
        }
    }
}
