using System;
using System.Collections.Generic;
using System.Text;

namespace Barter.li.Win.BL.Network
{
    class NetworkUriConstants
    {
        private const string APIBASEURL = "api.barter.li/api/";
        private const string APIVERSION = "v1/";
        private const String HTTP = @"http://";
        private const String HTTPS = @"https://";
        
        public static string BASE_URL = HTTP+ APIBASEURL + APIVERSION;
        public static string BASE_URL_SECURE = HTTPS + APIBASEURL + APIVERSION;

        public const string JSON_APPTYPE = "application/json";
        public const string FORM_POSTTYPE = "application/x-www-form-urlencoded";
    }
}
