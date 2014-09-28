//----------------------------------------------------------------------------------------------
// <copyright file="BarterLiApiRequestException.cs" company="BarterLi">
// Copyright (c) BarterLi.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------
namespace Barter.Li.Win.BarterliException
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Barter.Li.Win.BL.APIServices;

    /// <summary>
    /// BarterLiAPIsRequestException throws when API gives non success status-code
    /// </summary>
    public class BarterLiApiRequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BarterLiApiRequestException" /> class.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="url">API url in which error occurred</param>
        /// <param name="action">response action received by client API</param>
        /// <param name="isRetrying">flag indicating that network was retrying</param>
        /// <param name="reportToServer">flag indicating that whether this exception need to report to server</param>
        public BarterLiApiRequestException(string message, string url, ResponseAction action, bool isRetrying, bool reportToServer = false)
            : base(message)
        {
            this.Message = message;
            this.URL = url;
            this.StatusCode = action;
            this.IsRetrying = isRetrying;
            if (reportToServer)
            {
                this.LogToServer();
            }
        }

        /// <summary>
        /// Gets the Error message
        /// </summary>
        public new string Message { get; private set; }

        /// <summary>
        /// Gets the value of API url in which exception occurred
        /// </summary>
        public string URL { get; private set; }

        /// <summary>
        /// Gets the value of Response action sets by the API when network request failed
        /// </summary>
        public ResponseAction StatusCode { get; private set; }

        /// <summary>
        /// Gets a value indicating whether network was retrying when exception occurred
        /// </summary>
        public bool IsRetrying { get; private set; }

        /// <summary>
        /// Log bug to server
        /// </summary>
        private void LogToServer()
        {
            ////Log to Server
        }
    }
}
