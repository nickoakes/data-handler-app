using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace data_handler_app.GoogleAdsModels
{
    public partial class GoogleAdsCampaignRoot
    {
        [JsonProperty("LabelsAsCampaignLabelNames")]
        public List<object> LabelsAsCampaignLabelNames { get; set; }

        [JsonProperty("ResourceName")]
        public string ResourceName { get; set; }

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("ServingStatus")]
        public string ServingStatus { get; set; }

        [JsonProperty("AdServingOptimizationStatus")]
        public string AdServingOptimizationStatus { get; set; }

        [JsonProperty("AdvertisingChannelType")]
        public string AdvertisingChannelType { get; set; }

        [JsonProperty("AdvertisingChannelSubType")]
        public string AdvertisingChannelSubType { get; set; }

        [JsonProperty("TrackingUrlTemplate")]
        public object TrackingUrlTemplate { get; set; }

        [JsonProperty("UrlCustomParameters")]
        public List<object> UrlCustomParameters { get; set; }

        [JsonProperty("RealTimeBiddingSetting")]
        public object RealTimeBiddingSetting { get; set; }

        [JsonProperty("NetworkSettings")]
        public object NetworkSettings { get; set; }

        [JsonProperty("HotelSetting")]
        public object HotelSetting { get; set; }

        [JsonProperty("DynamicSearchAdsSetting")]
        public object DynamicSearchAdsSetting { get; set; }

        [JsonProperty("ShoppingSetting")]
        public object ShoppingSetting { get; set; }

        [JsonProperty("TargetingSetting")]
        public object TargetingSetting { get; set; }

        [JsonProperty("GeoTargetTypeSetting")]
        public object GeoTargetTypeSetting { get; set; }

        [JsonProperty("LocalCampaignSetting")]
        public object LocalCampaignSetting { get; set; }

        [JsonProperty("AppCampaignSetting")]
        public object AppCampaignSetting { get; set; }

        [JsonProperty("Labels")]
        public List<object> Labels { get; set; }

        [JsonProperty("ExperimentType")]
        public string ExperimentType { get; set; }

        [JsonProperty("BaseCampaign")]
        public object BaseCampaign { get; set; }

        [JsonProperty("CampaignBudget")]
        public object CampaignBudget { get; set; }

        [JsonProperty("BiddingStrategyType")]
        public string BiddingStrategyType { get; set; }

        [JsonProperty("StartDate")]
        public DateTimeOffset? StartDate { get; set; }

        [JsonProperty("EndDate")]
        public DateTimeOffset? EndDate { get; set; }

        [JsonProperty("FinalUrlSuffix")]
        public object FinalUrlSuffix { get; set; }

        [JsonProperty("FrequencyCaps")]
        public List<object> FrequencyCaps { get; set; }

        [JsonProperty("VideoBrandSafetySuitability")]
        public string VideoBrandSafetySuitability { get; set; }

        [JsonProperty("VanityPharma")]
        public object VanityPharma { get; set; }

        [JsonProperty("SelectiveOptimization")]
        public object SelectiveOptimization { get; set; }

        [JsonProperty("OptimizationGoalSetting")]
        public object OptimizationGoalSetting { get; set; }

        [JsonProperty("TrackingSetting")]
        public object TrackingSetting { get; set; }

        [JsonProperty("PaymentMode")]
        public string PaymentMode { get; set; }

        [JsonProperty("OptimizationScore")]
        public double? OptimizationScore { get; set; }

        [JsonProperty("BiddingStrategy")]
        public object BiddingStrategy { get; set; }

        [JsonProperty("Commission")]
        public object Commission { get; set; }

        [JsonProperty("ManualCpc")]
        public object ManualCpc { get; set; }

        [JsonProperty("ManualCpm")]
        public object ManualCpm { get; set; }

        [JsonProperty("ManualCpv")]
        public object ManualCpv { get; set; }

        [JsonProperty("MaximizeConversions")]
        public object MaximizeConversions { get; set; }

        [JsonProperty("MaximizeConversionValue")]
        public object MaximizeConversionValue { get; set; }

        [JsonProperty("TargetCpa")]
        public object TargetCpa { get; set; }

        [JsonProperty("TargetImpressionShare")]
        public object TargetImpressionShare { get; set; }

        [JsonProperty("TargetRoas")]
        public object TargetRoas { get; set; }

        [JsonProperty("TargetSpend")]
        public object TargetSpend { get; set; }

        [JsonProperty("PercentCpc")]
        public object PercentCpc { get; set; }

        [JsonProperty("TargetCpm")]
        public object TargetCpm { get; set; }

        [JsonProperty("CampaignBiddingStrategyCase")]
        public long? CampaignBiddingStrategyCase { get; set; }
    }
}