using data_handler_app.FacebookAdsModels;
using data_handler_app.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace data_handler_app.FacebookAdsMethods
{
    public static class InsightsMethods
    {
        public static async Task<dynamic> GetCampaignInsights(string campaignID, HttpClient client)
        {
            string url = $"{ConfigDictionary.Config()["FacebookAPIURLRoot"]}/{ConfigDictionary.Config()["FacebookAPIVersion"]}/{campaignID}/insights?date_preset=this_month&access_token={ConfigDictionary.Config()["FacebookAccessToken"]}";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                string responseData = await response.Content.ReadAsStringAsync();

                InsightsRoot insightsRoot = JsonConvert.DeserializeObject<InsightsRoot>(responseData);

                return insightsRoot;
            } 
            catch(Exception ex)
            {
                return ex;
            }
        }

        public static async Task<dynamic> ReturnFacebookAdsDTO(AllCampaignsRoot allCampaigns, HttpClient client)
        {
            List<CampaignDTO> campaignDTOList = new List<CampaignDTO>();

            List<string> errors = new List<string>();

            for(int i = 0; i < allCampaigns.Data.Count; i++)
            {
                try
                {
                    InsightsRoot insightsRoot = await GetCampaignInsights(allCampaigns.Data[i].Id, client);

                    if (insightsRoot.Data.Any())
                    {
                        campaignDTOList.Add(new CampaignDTO(insightsRoot));
                    }
                }
                catch(Exception ex)
                {
                    errors.Add(ex.Message);
                }
            }

            return new FacebookAdsDTO(campaignDTOList, errors);
        }
    }
}