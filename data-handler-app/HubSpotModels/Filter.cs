using Newtonsoft.Json;

namespace data_handler_app.HubSpotModels
{
    public class Filter
    {
        [JsonProperty("propertyName")]
        public string PropertyName { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
        public Filter(string propertyName, string op, string value)
        {
            PropertyName = propertyName;
            Operator = op;
            Value = value;
        }
    }
}