using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace data_handler_app.HubSpotModels
{
    public class ContactRoot
    {
        public partial class Contact
        {
            [JsonProperty("total")]
            public long Total { get; set; }

            [JsonProperty("results")]
            public List<Result> Results { get; set; }
        }

        public partial class Result
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("properties")]
            public Properties Properties { get; set; }

            [JsonProperty("createdAt")]
            public DateTimeOffset CreatedAt { get; set; }

            [JsonProperty("updatedAt")]
            public DateTimeOffset UpdatedAt { get; set; }

            [JsonProperty("archived")]
            public bool Archived { get; set; }
        }

        public partial class Properties
        {
            [JsonProperty("createdate")]
            public DateTimeOffset Createdate { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("firstname")]
            public string Firstname { get; set; }

            [JsonProperty("hs_object_id")]
            public string HsObjectId { get; set; }

            [JsonProperty("lastmodifieddate")]
            public DateTimeOffset Lastmodifieddate { get; set; }

            [JsonProperty("lastname")]
            public string Lastname { get; set; }

            [JsonProperty("phone")]
            public string Phone { get; set; }
        }
    }
}