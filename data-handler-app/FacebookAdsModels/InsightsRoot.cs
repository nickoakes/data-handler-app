using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace data_handler_app.FacebookAdsModels
{
    public partial class InsightsRoot
    {
        [JsonProperty("data")]
        public List<Datum> Data { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("campaign_id")]
        public string CampaignId { get; set; }

        [JsonProperty("date_start")]
        public DateTimeOffset DateStart { get; set; }

        [JsonProperty("date_stop")]
        public DateTimeOffset DateStop { get; set; }

        [JsonProperty("impressions")]
        public long Impressions { get; set; }

        [JsonProperty("spend")]
        public double Spend { get; set; }
    }
}