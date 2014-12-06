using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barter.li.Win.Model
{
    public class Book
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("publication_year")]
        public int PublicationYear { get; set; }

        [JsonProperty("publication_month")]
        public string PublicationMonth { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("barter_type")]
        public object BarterType { get; set; }

        [JsonProperty("location")]
        public object Location { get; set; }

        [JsonProperty("tags")]
        public object[] Tags { get; set; }

        [JsonProperty("id_book")]
        public object IdBook { get; set; }
    }
}
