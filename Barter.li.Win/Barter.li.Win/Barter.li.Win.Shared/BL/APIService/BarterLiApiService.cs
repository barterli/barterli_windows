using Barter.li.Win.BL.Network;
using Barter.li.Win.Util;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Barter.li.Win.BL.APIService
{
    public class BarterLiApiService
    {

        public enum DataSource
        {
            Cache,
            Network
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BarterLiApiService() { }

        /// <summary>
        /// SendRequest and Receive Callbacks for DataReceived/ErrorOccured
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="networkContext"></param>
        /// <param name="onDataReceived"></param>
        /// <param name="onError"></param>
        public async void SendRequest<T>(NetworkContext networkContext, Action<DataSource, T> onDataReceived, Action<Exception> onError)
        {

            if (onError == null)
            {
                throw new ArgumentException("Parameter Can not be null", "Action<Exception> onError");
            }

            if (onDataReceived == null)
            {
                throw new ArgumentException("Parameter Can not be null", "Action<<DataSource,T> onDataReceived");
            }

            try
            {
                T value = await SendRequestAsync<T>(networkContext);
                if (value != null)
                {
                    onDataReceived(DataSource.Network, value);
                }
                else
                {
                    onError(new Exception("Error occured"));
                }
            }
            catch (Exception e)
            {
                onError(e);
            }
        }

        /// <summary>
        /// Async Method for send request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="networkContext"></param>
        /// <returns></returns>
        public async Task<T> SendRequestAsync<T>(NetworkContext networkContext)
        {
            T value;
            string url = networkContext.URL;
            HttpMethod method = networkContext.httpMethod;
            string post = networkContext.post;
            bool isSecureConnection = networkContext.isSecureConnection;
            int retryCount = networkContext.retryCount;
            CancellationToken token = networkContext.cancellationToken;

            if (networkContext.Equals(null))
            {
                throw new ArgumentException("Parameter Can not be null", "NetworkContext");
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("Parameter Can not be null", "NetworkConstant.URL");
            }

            NetworkService ns = new NetworkService();
            HttpResponseMessage response = await ns.SendAsync(url, method, post, isSecureConnection, token);

            string responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                value = await ResponseConverter.Deserialize<T>(responseString);
            }
            else
            {
                throw new Exception(" Following Error occured : " + responseString);
            }
            return value;
        }

    }
}
