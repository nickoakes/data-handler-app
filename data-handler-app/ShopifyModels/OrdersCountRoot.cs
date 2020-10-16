using Newtonsoft.Json;

namespace data_handler_app.ShopifyModels
{
    public struct OrdersCountRoot
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}