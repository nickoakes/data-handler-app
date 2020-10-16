using Newtonsoft.Json;
using System;

namespace data_handler_app.ShopifyModels
{
    public partial class OrderJSONLRoot
    {
        [JsonProperty("processedAt")]
        public DateTimeOffset ProcessedAt { get; set; }

        [JsonProperty("amount")]
        public Amount Amount { get; set; }
    }

    public partial class Amount
    {
        [JsonProperty("amount")]
        public decimal AmountValue { get; set; }
    }
}