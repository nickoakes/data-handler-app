using Newtonsoft.Json;
using System;

namespace data_handler_app.ClickUpModels
{
    public partial class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("initials")]
        public string Initials { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("profilePicture")]
        public Uri ProfilePicture { get; set; }
    }
}