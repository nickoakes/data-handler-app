using Newtonsoft.Json;
using System;

namespace data_handler_app.GoogleAdsModels
{
    public partial class GoogleAdsBudgetRoot
    {
        [JsonProperty("ResourceName")]
        public string ResourceName { get; set; }

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("BillingSetup")]
        public string BillingSetup { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ProposedStartDateTime")]
        public DateTimeOffset? ProposedStartDateTime { get; set; }

        [JsonProperty("ApprovedStartDateTime")]
        public DateTimeOffset? ApprovedStartDateTime { get; set; }

        [JsonProperty("TotalAdjustmentsMicros")]
        public long? TotalAdjustmentsMicros { get; set; }

        [JsonProperty("AmountServedMicros")]
        public long? AmountServedMicros { get; set; }

        [JsonProperty("PurchaseOrderNumber")]
        public string PurchaseOrderNumber { get; set; }

        [JsonProperty("Notes")]
        public string Notes { get; set; }

        [JsonProperty("PendingProposal")]
        public string PendingProposal { get; set; }

        [JsonProperty("ProposedEndDateTime")]
        public DateTimeOffset? ProposedEndDateTime { get; set; }

        [JsonProperty("ProposedEndTimeType")]
        public string ProposedEndTimeType { get; set; }

        [JsonProperty("ApprovedEndDateTime")]
        public DateTimeOffset? ApprovedEndDateTime { get; set; }

        [JsonProperty("ApprovedEndTimeType")]
        public string ApprovedEndTimeType { get; set; }

        [JsonProperty("ProposedSpendingLimitMicros")]
        public long? ProposedSpendingLimitMicros { get; set; }

        [JsonProperty("ProposedSpendingLimitType")]
        public string ProposedSpendingLimitType { get; set; }

        [JsonProperty("ApprovedSpendingLimitMicros")]
        public long? ApprovedSpendingLimitMicros { get; set; }

        [JsonProperty("ApprovedSpendingLimitType")]
        public string ApprovedSpendingLimitType { get; set; }

        [JsonProperty("AdjustedSpendingLimitMicros")]
        public long? AdjustedSpendingLimitMicros { get; set; }

        [JsonProperty("AdjustedSpendingLimitType")]
        public string AdjustedSpendingLimitType { get; set; }

        [JsonProperty("ProposedEndTimeCase")]
        public long? ProposedEndTimeCase { get; set; }

        [JsonProperty("ApprovedEndTimeCase")]
        public long? ApprovedEndTimeCase { get; set; }

        [JsonProperty("ProposedSpendingLimitCase")]
        public long? ProposedSpendingLimitCase { get; set; }

        [JsonProperty("ApprovedSpendingLimitCase")]
        public long? ApprovedSpendingLimitCase { get; set; }

        [JsonProperty("AdjustedSpendingLimitCase")]
        public long? AdjustedSpendingLimitCase { get; set; }
    }
}