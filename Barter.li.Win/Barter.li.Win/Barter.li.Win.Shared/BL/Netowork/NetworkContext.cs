//----------------------------------------------------------------------------------------------
// <copyright file="NetworkContext.cs" company="BarterLi">
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

    /// <summary>
    /// Network Context
    /// </summary>
    public class NetworkContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkContext"/> class.
        /// </summary>
        public NetworkContext()
        {
            this.URL = string.Empty;
            this.Post = string.Empty;
            this.HttpMethod = System.Net.Http.HttpMethod.Get;
            this.IsSecureConnection = false;
            this.RetryCount = 1;       
        }

        /// <summary>
        /// Gets or sets Cancellation Token to control the network request
        /// </summary>
        internal CancellationToken CancellationToken
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Type of HTTP method
        /// Get, Set, Put, Post, Delete
        /// </summary>
        internal HttpMethod HttpMethod
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether network using SecureConnection
        /// True if network request must be place using HTTPS protocol
        /// </summary>
        internal bool IsSecureConnection
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Post Data
        /// Post form data if Network request is type of Post
        /// </summary>
        internal string Post
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the RetryCount 
        /// No of retry for network request in case of failure response
        /// </summary>
        internal int RetryCount
        {
            get;
            set;
        }
       
        /// <summary>
        /// Gets or sets Relative url of API except base url and version
        /// e.g API/Search/1
        /// </summary>
        internal string URL
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Request-Id       
        /// </summary>
        internal int RequestId
        {
            get;
            set;
        }
    }
}