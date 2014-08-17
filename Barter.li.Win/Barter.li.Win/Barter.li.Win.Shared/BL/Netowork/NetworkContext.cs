using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace Barter.li.Win.BL.Network
{
    public class NetworkContext
    {
        public NetworkContext()
        {
            URL = "";
            post = "";
            httpMethod = System.Net.Http.HttpMethod.Get;
            isSecureConnection = false;
            retryCount = 0;           
        }

        internal CancellationToken cancellationToken
        {
            get;
            set;
        }       
      
        internal HttpMethod httpMethod
        {
            get;
            set;
        }

        internal bool isSecureConnection
        {
            get;
            set;
        }

       
        internal string post
        {
            get;
            set;
        }

        internal int retryCount
        {
            get;
            set;
        }
       
        internal string URL
        {
            get;
            set;
        }
    }
}
