using Newtonsoft.Json;
using System;

namespace data_handler_app.ShopifyModels
{
    public partial class BulkOperationStatusRoot
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("extensions")]
        public Extensions Extensions { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("currentBulkOperation")]
        public CurrentBulkOperation CurrentBulkOperation { get; set; }
    }

    public partial class CurrentBulkOperation
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("completedAt")]
        public DateTimeOffset? CompletedAt { get; set; }

        [JsonProperty("objectCount")]
        public int ObjectCount { get; set; }

        [JsonProperty("fileSize")]
        public string FileSize { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("partialDataUrl")]
        public string PartialDataUrl { get; set; }
    }

    public partial class Extensions
    {
        [JsonProperty("cost")]
        public Cost Cost { get; set; }
    }

    public partial class Cost
    {
        [JsonProperty("requestedQueryCost")]
        public int RequestedQueryCost { get; set; }

        [JsonProperty("actualQueryCost")]
        public int ActualQueryCost { get; set; }

        [JsonProperty("throttleStatus")]
        public ThrottleStatus ThrottleStatus { get; set; }
    }

    public partial class ThrottleStatus
    {
        [JsonProperty("maximumAvailable")]
        public decimal MaximumAvailable { get; set; }

        [JsonProperty("currentlyAvailable")]
        public decimal CurrentlyAvailable { get; set; }

        [JsonProperty("restoreRate")]
        public decimal RestoreRate { get; set; }
    }
}