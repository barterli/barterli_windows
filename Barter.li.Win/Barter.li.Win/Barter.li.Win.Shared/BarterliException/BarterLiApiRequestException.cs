using Barter.Li.Win.BL.APIServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barter.Li.Win.BarterliException
{
    public class BarterLiApiRequestException : Exception
    {
        public new string Message { get; private set; }
        public string URL { get; private set; }
        public ResponseAction statusCode { get; private set; }
        public bool IsRetrying {get; private set;}

        public BarterLiApiRequestException(string message, string url, ResponseAction action, bool isRetrying, bool reportToServer = false)
            : base(message)
        {
            this.Message = message;
            this.URL = url;
            this.statusCode = action;
            this.IsRetrying = IsRetrying;
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
