//----------------------------------------------------------------------------------------------
// <copyright file="NetworkUriConstants.cs" company="BarterLi">
// Copyright (c) BarterLi.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------
namespace Barter.Li.Win.BL.Network
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// All the Base URL
    /// </summary>
    public class NetworkUriConstants
    {
        /// <summary>
        /// Base URL
        /// </summary>
        private const string APIBASEURL = "api.barter.li/api/";

        /// <summary>
        /// API Version
        /// </summary>
        private const string APIVERSION = "v1/";

        /// <summary>
        /// Protocol prefix string
        /// </summary>
        private const string HTTP = @"http://";

        /// <summary>
        /// Secure protocol prefix string
        /// </summary>
        private const string HTTPS = @"https://";

        /// <summary>
        /// Gets Combine protocol prefix, base url and version in form of URL
        /// </summary>
        public static string BASEURL
        {
            get
            {
                return HTTP + APIBASEURL + APIVERSION;
            }
        }

        /// <summary>
        /// Gets Combine secure protocol prefix, base url and version in form of URL
        /// </summary>
        public static string BASEURLSECURE
        {
            get
            {
                return HTTPS + APIBASEURL + APIVERSION;
            }
        }

        /// <summary>
        /// Gets Accept JSON Type string
        /// </summary>
        public static string JSONAPPTYPE
        {
            get
            {
                return "application/json";
            }
        }

        /// <summary>
        /// Gets Header string for From post
        /// </summary>
        public static string FORMPOSTTYPE
        {
            get
            {
                return "application/x-www-form-urlencoded";
            }
        }
    }
}
