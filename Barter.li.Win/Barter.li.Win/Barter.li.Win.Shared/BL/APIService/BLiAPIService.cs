﻿using Barter.li.Win.BarterliException;
using Barter.li.Win.BL.Network;
using Barter.li.Win.Util;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Barter.li.Win.BL.APIServices
{
    public partial class BliAPIService
    {
        /* under dev*/

        public enum DataSource
        {
            Cache,
            Network
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BliAPIService() { }

        /// <summary>
        /// SendRequest and Receive Callbacks for DataReceived/ErrorOccured
        /// Any UI operation is suggested in Dispatcher thread as it is Background thread operatrion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="networkContext"></param>
        /// <param name="onDataReceived"></param>
        /// <param name="onError"></param>
        public void SendRequest<T>(NetworkContext networkContext, Action<DataSource, T> onDataReceived, Action<Exception> onError)
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
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    T value = Task.Run(() => SendRequestAsync<T>(networkContext)).Result;

                    if (value != null)
                    {
                        onDataReceived(DataSource.Network, value);
                    }
                    else
                    {
                        onError(new Exception("Error occured"));
                    }
                });
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
            ResponseAction currentAction = ResponseAction.RETRY;
            string errorMessage = "";
            if (networkContext.retryCount == 0)
            {
                throw new BarterliException.BarterLiApiRequestException(errorMessage, networkContext.URL, currentAction);
            }

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
                throw new ArgumentException("Parameter Can not be null", "NetworkContext.URL");
            }

            NetworkService ns = new NetworkService();
            HttpResponseMessage response = await ns.SendAsync(url, method, post, isSecureConnection, token);

            string responseString = await response.Content.ReadAsStringAsync();
            var isRetrying = networkContext.retryCount > 0 ? true : false;
            currentAction = GetStatus(response.StatusCode, isRetrying);

            switch (currentAction)
            {
                case ResponseAction.SUCCESS:
                    return await ResponseConverter.Deserialize<T>(responseString);                    
                case ResponseAction.RETRY:
                    networkContext.retryCount--;
                    await SendRequestAsync<T>(networkContext);
                    break;
                case ResponseAction.STOP:
                case ResponseAction.BAD_REQUEST:
                    errorMessage = responseString;
                    break;
            }
            throw new BarterLiApiRequestException(responseString, networkContext.URL, currentAction);
            
        }

    }
}
