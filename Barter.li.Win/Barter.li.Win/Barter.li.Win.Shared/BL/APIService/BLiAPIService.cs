//----------------------------------------------------------------------------------------------
// <copyright file="BLiAPIService.cs" company="BarterLi">
// Copyright (c) BarterLi.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------
namespace Barter.Li.Win.BL.APIServices
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Barter.Li.Win.BarterliException;
    using Barter.Li.Win.BL.Network;
    using Barter.Li.Win.Util;
    using Windows.UI.Core;
    using Barter.li.Win.BL.Netowork;

    /// <summary>
    ///     API service
    ///     Responsible for 
    ///     handle the Request
    ///     Find the nearest data
    ///     respond to caller with data and data source
    /// </summary>
    public partial class APIService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="APIService" /> class.     
        /// </summary>
        public APIService()
        {
        }

        /// <summary>
        ///     Type of Source of Response
        /// </summary>
        public enum DataSource
        {
            /// <summary>
            ///     Retrieved from Database or Memory
            /// </summary>
            Cache,

            /// <summary>
            ///     From Network Response
            /// </summary>
            Network
        }

        /// <summary>
        ///     Async Method for send request
        /// </summary>
        /// <typeparam name="T">Expecting Type</typeparam>
        /// <param name="networkContext">NetworkContext with all required properties</param>
        /// <returns>Reference of Expecting Type</returns>
        public ApiResponse<T> SendRequestAsync<T>(NetworkContext networkContext)
        {
            ResponseAction currentAction = ResponseAction.RETRY;
            string errorMessage = string.Empty;
            if (networkContext.RetryCount == 0)
            {
                throw new BarterliException.BarterLiApiRequestException(errorMessage, networkContext.URL, currentAction, false);
            }

            ApiResponse<T> apiResponse = new ApiResponse<T>() { Id = networkContext.RequestId };

            string url = networkContext.URL;
            HttpMethod method = networkContext.HttpMethod;
            string post = networkContext.Post;
            bool isSecureConnection = networkContext.IsSecureConnection;
            int retryCount = networkContext.RetryCount;
            CancellationToken token = networkContext.CancellationToken;

            if (networkContext.Equals(null))
            {
                throw new ArgumentException("Parameter Can not be null", "NetworkContext");
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("Parameter Can not be null", "URL");
            }

            if (networkContext.RequestId == 0 )
            {
                throw new ArgumentException("Parameter must be non-zero value", "RequestId");
            }

            NetworkService ns = new NetworkService();
            HttpResponseMessage response = ns.SendAsync(url, method, post, isSecureConnection, token).Result;
            string responseString = response.Content.ReadAsStringAsync().Result;

            var isRetrying = networkContext.RetryCount > 0 ? true : false;
            currentAction = GetStatus(response.StatusCode, isRetrying);

            if (currentAction == ResponseAction.STOP || currentAction == ResponseAction.BAD_REQUEST)
            {
                errorMessage = responseString;
            }
            else if (currentAction == ResponseAction.RETRY)
            {
                networkContext.RetryCount--;
                this.SendRequestAsync<T>(networkContext);
            }
            else if (currentAction == ResponseAction.SUCCESS)
            {
                apiResponse.value = ResponseConverter.Deserialize<T>(responseString).Result;
                if (apiResponse == null && isRetrying)
                {
                    networkContext.RetryCount--;
                    this.SendRequestAsync<T>(networkContext);
                }
                else
                {
                    return apiResponse;
                }
            }

            throw new BarterLiApiRequestException(responseString, networkContext.URL, currentAction, isRetrying);
        }
    }
}
