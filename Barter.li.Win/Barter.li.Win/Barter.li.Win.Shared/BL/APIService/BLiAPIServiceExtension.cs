//----------------------------------------------------------------------------------------------
// <copyright file="BLiAPIServiceExtension.cs" company="BarterLi">
// Copyright (c) BarterLi.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------
namespace Barter.Li.Win.BL.APIServices
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    /// <summary>
    /// Type of Response actions
    /// </summary>
    public enum ResponseAction
    {
        /// <summary>
        /// Success - 200
        /// </summary>
        SUCCESS,

        /// <summary>
        /// is Network retrying
        /// </summary>
        RETRY,

        /// <summary>
        /// Indicate to stop after failure
        /// </summary>
        STOP,

        /// <summary>
        /// Bad request status from APIS
        /// </summary>
        BAD_REQUEST,
    }

    /// <summary>
    /// Partial class of API service
    /// </summary>
    public partial class APIService
    {
        /* Under dev */

        /// <summary>
        /// Get the Response action based on HttpStatusCode
        /// </summary>
        /// <param name="statusCode">HttpStatusCode send by network</param>
        /// <param name="isRetrying">true if network is still retrying, else false</param>
        /// <returns>Response action to perform on network or caller</returns>
        private ResponseAction GetStatus(HttpStatusCode statusCode, bool isRetrying)
        {
            if (statusCode == HttpStatusCode.OK)
            {
                return ResponseAction.SUCCESS;
            }
            else if (statusCode == HttpStatusCode.NoContent)
            {
                if (isRetrying)
                {
                    return ResponseAction.RETRY;
                }
            }
            else if (statusCode == HttpStatusCode.NotFound)
            {
                if (isRetrying)
                {
                    return ResponseAction.RETRY;
                }
            }

            return ResponseAction.STOP;
        }
    }
}
