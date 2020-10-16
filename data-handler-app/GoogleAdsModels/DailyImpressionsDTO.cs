using Google.Ads.GoogleAds.V4.Services;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Linq;

namespace data_handler_app.GoogleAdsModels
{
    public class DailyImpressionsDTO
    {
        public List<CompanyDailyImpressionsSummary> DailyImpressionsSummary { get; set; }
    }

    public class CompanyDailyImpressionsSummary
    {
        public string Name { get; set; }
        public List<CampaignDailyImpressionsSummary> Campaigns { get; set; }
        public CompanyDailyImpressionsSummary(string companyName, RepeatedField<GoogleAdsRow> resultsRoot)
        {
            Name = companyName;

            var campaigns = new List<CampaignDailyImpressionsSummary>();

            foreach(GoogleAdsRow row in resultsRoot.Where(x => x.Metrics.Impressions > 0 && x.Metrics.Impressions < 100))
            {
                campaigns.Add(new CampaignDailyImpressionsSummary(row.Campaign.Name, row.Metrics.Impressions));
            }

            Campaigns = campaigns;
        }
    }
    
    public class CampaignDailyImpressionsSummary
    {
        public string Name { get; set; }
        public long? Impressions { get; set; }
        public CampaignDailyImpressionsSummary(string name, long? impressions)
        {
            Name = name;
            Impressions = impressions;
        }
    }
}