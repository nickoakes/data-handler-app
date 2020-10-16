using Google.Ads.GoogleAds.V4.Services;
using data_handler_app.GoogleAdsMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace data_handler_app.GoogleAdsModels
{
    public class GoogleAdsCampaignSummaryDTO
    {
        public double? TotalSpendThisMonth { get; set; }
        public double? TotalConversions { get; set; }
        public double? TotalConversionsValue { get; set; }
        public double? ROAS { get; set; }
        public double? PreviousMonthROAS { get; set; }
        public string InACampaign { get; set; }
        public List<GoogleAdsCampaignDTO> Campaigns { get; set; }
        public GoogleAdsCampaignSummaryDTO(List<GoogleAdsCampaignDTO> campaigns,
                                           double? totalSpend,
                                           double? totalConversions,
                                           double? totalConversionsValue,
                                           double? rOAS,
                                           double? prevMonthROAS)
        {
            TotalSpendThisMonth = totalSpend;
            TotalConversions = totalConversions;
            TotalConversionsValue = totalConversionsValue;
            ROAS = rOAS;
            PreviousMonthROAS = prevMonthROAS;
            InACampaign = campaigns.Any() ? "Currently in a campaign" : "Not currently in a campaign";
            Campaigns = campaigns;
        }
    }
    public class GoogleAdsCampaignDTO
    {
        public GoogleAdsCampaign Campaign { get; set; }
        public GoogleAdsMetrics Metrics { get; set; }
        public GoogleAdsCampaignDTO(GoogleAdsRow result)
        {
            Campaign = new GoogleAdsCampaign(result);
            Metrics = new GoogleAdsMetrics(result);
        }
    }

    public class GoogleAdsCampaign
    {
        public long? ID { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public GoogleAdsCampaign(GoogleAdsRow result)
        {
            ID = result.Campaign.Id;
            Name = result.Campaign.Name;
            StartDate = BudgetMethods.ConvertStringToDateTime(result.Campaign.StartDate);
            EndDate = BudgetMethods.ConvertStringToDateTime(result.Campaign.EndDate);
        }
    }

    public class GoogleAdsMetrics
    {
        public long? Impressions { get; set; }
        public long? Clicks { get; set; }
        public double? ConversionsValue { get; set; }
        public double? Conversions { get; set; }
        public double? TotalCost { get; set; }
        public GoogleAdsMetrics(GoogleAdsRow result)
        {
            Impressions = result.Metrics.Impressions;
            Clicks = result.Metrics.Clicks;
            ConversionsValue = result.Metrics.ConversionsValue;
            Conversions = result.Metrics.Conversions;
            TotalCost = BudgetMethods.ConvertMicroToDouble(result.Metrics.CostMicros);
        }
    }
}