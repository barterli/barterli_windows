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
        /// <returns>HttpResponseMessage return by API</returns>
        internal async Task<HttpResponseMessage> SendAsync(string apiUrl, HttpMethod httpMethod, string postBody, bool isSecureConnection, CancellationToken token)
        {
            HttpClient client = new HttpClient();
            string navigationUrl = isSecureConnection ? NetworkUriConstants.BASEURLSECURE : NetworkUriConstants.BASEURL;
            navigationUrl = navigationUrl + apiUrl;
            HttpContent content = null;
            if (httpMethod != HttpMethod.Get || httpMethod != HttpMethod.Delete)
            {
                content = new StringContent(
                    postBody,
                    Encoding.UTF8,
                    NetworkUriConstants.JSONAPPTYPE);
            }

            client.Timeout = TimeSpan.FromSeconds(10.00);

            HttpResponseMessage response;
            try
            {
                if (object.Equals(httpMethod, HttpMethod.Post))
                {
                    response = await client.PostAsync(navigationUrl, content, token);
                }
                else if (object.Equals(httpMethod, HttpMethod.Put))
                {
                    response = await client.PutAsync(navigationUrl, content, token);
                }
                else if (object.Equals(httpMethod, HttpMethod.Delete))
                {
                    response = await client.DeleteAsync(navigationUrl, token);
                }
                else
                {
                    response = await client.GetAsync(navigationUrl, token);
                }

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
    }
}
