//----------------------------------------------------------------------------------------------
// <copyright file="NetworkService.cs" company="BarterLi">
// Copyright (c) BarterLi.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------
namespace Barter.Li.Win.BL.Network
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Network service class
    /// Place request on network
    /// </summary>
    internal class NetworkService
    {
        /// <summary>
        /// Place request on network as an asynchronous operation
        /// Throws <see cref="TaskCanceledException"/> in case of Task is cancelled
        /// Throws <see cref="Exception"/> in case of exception occurred in network request
        /// </summary>
        /// <param name="apiUrl">e.g API/v1/search</param>
        /// <param name="httpMethod">type of method e.g Get, Post, Put, Delete</param>
        /// <param name="postBody">Post body content in case of post http method</param>
        /// <param name="isSecureConnection">flag indicating whether to use HTTP or HTTPS</param>
        /// <param name="token">Cancellation token to control network request</param>
        /// <param name="headers">Optional - required Headers for network request</param>
        /// <returns>HttpResponseMessage return by API</returns>
        internal async Task<HttpResponseMessage> SendAsync(string apiUrl, HttpMethod httpMethod, string postBody, bool isSecureConnection, CancellationToken token, Dictionary<string, string> headers = null)
        {
            HttpClient client = new HttpClient();
            string navigationUrl = isSecureConnection ? NetworkUriConstants.BASEURLSECURE : NetworkUriConstants.BASEURL;
            navigationUrl = navigationUrl + apiUrl;
            HttpContent content = null;
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            if (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put)
            {
                content = new StringContent(
                    postBody,
                    Encoding.UTF8,
                    NetworkUriConstants.JSONAPPTYPE);
                requestMessage.Content = content;
            }

            if (headers != null && headers.Count > 0)
            {
                this.SetHeaders(requestMessage, headers);
            }

            requestMessage.RequestUri = new Uri(navigationUrl);
            client.Timeout = TimeSpan.FromSeconds(10.00);

            HttpResponseMessage response;
            try
            {
                if (object.Equals(httpMethod, HttpMethod.Post))
                {
                    requestMessage.Method = HttpMethod.Post;
                }
                else if (object.Equals(httpMethod, HttpMethod.Put))
                {
                    requestMessage.Method = HttpMethod.Put;
                }
                else if (object.Equals(httpMethod, HttpMethod.Delete))
                {
                    requestMessage.Method = HttpMethod.Delete;
                }
                else
                {
                    requestMessage.Method = HttpMethod.Get;
                }

                response = await client.SendAsync(requestMessage, token);

                return response;
            }
            catch (TaskCanceledException tce)
            {
                throw tce;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Set the headers in request
        /// </summary>
        /// <param name="requestMessage">RequestMessage object to set the headers</param>
        /// <param name="headers">KVPair of headers to add in request message</param>
        private void SetHeaders(HttpRequestMessage requestMessage, Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                ////to prevent duplicate key excption
                ////remove the header if key is present 
                ////and add it with new value
                if (requestMessage.Headers.Contains(header.Key))
                {
                    requestMessage.Headers.Remove(header.Key);
                }

                requestMessage.Headers.Add(header.Key, header.Value);
            }
        }
    }
}
