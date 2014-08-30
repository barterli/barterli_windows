using Barter.li.Win.BL.APIServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barter.li.Win.BarterliException
{
    public class BarterLiApiRequestException : Exception
    {
        public string Message { get; private set; }
        public string URL { get; private set; }
        public ResponseAction statusCode { get; private set; }

        public BarterLiApiRequestException(string message, string url, ResponseAction action, bool reportToServer = false)
            : base(message)
        {
            this.Message = message;
            this.URL = url;
            this.statusCode = action;
            if(reportToServer)
            {
                LogToServer();
            }
        }

        private void LogToServer()
        {
            //Log to Server
        }
    }
}
