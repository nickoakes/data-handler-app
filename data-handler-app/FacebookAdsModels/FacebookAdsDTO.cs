using System;
using System.Collections.Generic;
using System.Linq;

namespace data_handler_app.FacebookAdsModels
{
    public class FacebookAdsDTO
    {
        public double TotalSpend { get; set; }
        public long TotalImpressions { get; set; }
        public List<CampaignDTO> Campaigns { get; set; }
        public List<string> Errors { get; set; }
        public FacebookAdsDTO(List<CampaignDTO> campaigns, List<string> errors)
        {
            double totalSpend = 0;
            long totalImpressions = 0;

            for(int i = 0; i < campaigns.Count; i++)
            {
                totalSpend += campaigns[i].Spend;
                totalImpressions += campaigns[i].Impressions;
            }

            TotalSpend = totalSpend;
            TotalImpressions = totalImpressions;
            Campaigns = campaigns;
            Errors = errors;
        }
    }

    public class CampaignDTO
    {
        public string CampaignID { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public long Impressions { get; set; }
        public double Spend { get; set; }
        public CampaignDTO(InsightsRoot root)
        {
            if (root.Data.Any())
            {
                CampaignID = root.Data.FirstOrDefault().CampaignId;
                StartDate = root.Data.FirstOrDefault().DateStart;
                EndDate = root.Data.FirstOrDefault().DateStop;
                Impressions = root.Data.FirstOrDefault().Impressions;
                Spend = root.Data.FirstOrDefault().Spend;
            }
            else
            {
                CampaignID = string.Empty;
                StartDate = null;
                EndDate = null;
                Impressions = 0;
                Spend = 0;
            }
        }
    }
}