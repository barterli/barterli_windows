using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barter.li.Win.Model
{
    public class RequestJsonTypes
    {
        public class ResetPasswordRequest
        {
            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }

            [JsonProperty("token")]
            public string Token { get; set; }
        }
    }
}