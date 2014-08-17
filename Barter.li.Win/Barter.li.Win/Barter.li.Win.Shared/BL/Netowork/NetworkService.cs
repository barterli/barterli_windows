using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Barter.li.Win.BL.Network
{
    class NetworkService
    {

        internal async Task<HttpResponseMessage> SendAsync(string apiUrl, HttpMethod httpMethod, string postBody, bool isSecureConnection, CancellationToken token)
        {
            HttpClient client = new HttpClient();
            string navigationUrl = isSecureConnection ? NetworkUriConstants.BASE_URL_SECURE : NetworkUriConstants.BASE_URL;
            navigationUrl = navigationUrl + apiUrl;
            HttpContent content = null;
            if (httpMethod != HttpMethod.Get || httpMethod != HttpMethod.Delete)
            {
                content = new StringContent(
                    postBody,
                    Encoding.UTF8,
                    NetworkUriConstants.JSON_APPTYPE);
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
