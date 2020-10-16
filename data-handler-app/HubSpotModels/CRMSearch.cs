using Newtonsoft.Json;
using System.Collections.Generic;

namespace data_handler_app.HubSpotModels
{
    public class RequestBody
    {
        [JsonProperty("filters")]
        public List<Filter> Filters { get; set; }

        [JsonProperty("properties")]
        public List<string> Properties { get; set; }

        public RequestBody(List<Filter> filters, List<string> properties)
        {
            Filters = filters;
            Properties = properties;
        }
    }
}