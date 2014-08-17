namespace Barter.li.Win.Model.SearchResponseJsonTypes
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Barter.li.Win.Model.SearchResponseJsonTypes;


    public class Location
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("locality")]
        public object Locality { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("id_location")]
        public string IdLocation { get; set; }

        [JsonProperty("foursquare_id")]
        public string FoursquareId { get; set; }
    }

    public class Search
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("publication_year")]
        public int? PublicationYear { get; set; }

        [JsonProperty("publication_month")]
        public object PublicationMonth { get; set; }

        [JsonProperty("value")]
        public int? Value { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("barter_type")]
        public object BarterType { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("id_book")]
        public string IdBook { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isbn_10")]
        public object Isbn10 { get; set; }

        [JsonProperty("isbn_13")]
        public string Isbn13 { get; set; }

        [JsonProperty("id_user")]
        public string IdUser { get; set; }

        [JsonProperty("owner_name")]
        public string OwnerName { get; set; }

        [JsonProperty("owner_image_url")]
        public string OwnerImageUrl { get; set; }

        [JsonProperty("image_present")]
        public bool ImagePresent { get; set; }
    }


    public class SearchResponse
    {

        [JsonProperty("search")]
        public Search[] Search { get; set; }
    }

}
