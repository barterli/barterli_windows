using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barter.li.Win.Model
{
    public partial class UserInfoJsonTypes
    {

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
    }

    public class User
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("location")]
        public object Location { get; set; }

        [JsonProperty("auth_token")]
        public string AuthToken { get; set; }

        [JsonProperty("sign_in_count")]
        public int SignInCount { get; set; }

        [JsonProperty("id_user")]
        public object IdUser { get; set; }

        [JsonProperty("books")]
        public Book[] Books { get; set; }
    }
}
