using data_handler_app.FacebookAdsModels;
using data_handler_app.Shared;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace data_handler_app.FacebookAdsMethods
{
    public static class CampaignsMethods
    {
        public static async Task<dynamic> GetAllCampaigns(string companyName, Entities db, HttpClient client)
        {
            string alias;

            try
            {
                alias = AliasMethods.GetAlias(companyName, db, "FacebookAdAccountID");
            }
            catch
            {
                return new ArgumentException($"No HubSpot alias found for the company name {companyName}");
            }

            string url = $"{ConfigDictionary.Config()["FacebookAPIURLRoot"]}/{ConfigDictionary.Config()["FacebookAPIVersion"]}/act_{alias}/campaigns?access_token={ConfigDictionary.Config()["FacebookAccessToken"]}";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                string responseData = await response.Content.ReadAsStringAsync();

                AllCampaignsRoot allCampaignsRoot = JsonConvert.DeserializeObject<AllCampaignsRoot>(responseData);

                return allCampaignsRoot;
            }
            catch (HttpRequestException ex)
            {
                return ex;
            }
        }
    }
}