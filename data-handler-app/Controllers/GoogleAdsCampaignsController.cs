using System.Diagnostics;
using System.Web.Http;
using Google.Ads.GoogleAds;
using Google.Ads.GoogleAds.Lib;
using Google.Ads.GoogleAds.V4.Services;
using Google.Ads.GoogleAds.V4.Errors;
using data_handler_app.Shared;
using System;
using Google.Protobuf.Collections;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using data_handler_app.GoogleAdsMethods;
using data_handler_app.GoogleAdsModels;

namespace data_handler_app.Controllers
{
    public class GoogleAdsCampaignsController : ApiController
    {
        private bool CheckClientSecret()
        {
            IEnumerable<string> headerValuesClientSecret;
            IEnumerable<string> headerValuesClientID;

            if (Request
               .Headers
               .TryGetValues("client-secret", out headerValuesClientSecret) &&
               Request
               .Headers
               .TryGetValues("client-id", out headerValuesClientID))
            {
                if (string.Equals(headerValuesClientSecret.FirstOrDefault(), ConfigDictionary.Config()["ClientSecret"]) &&
                    string.Equals(headerValuesClientID.FirstOrDefault(), ConfigDictionary.Config()["ClientID"]))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public dynamic Get(string companyName)
        {
            if (CheckClientSecret())
            {
                string customerID;

                try
                {
                    using(Entities db = new Entities())
                    {
                        customerID = AliasMethods.GetAlias(companyName, db, "GoogleAdsCustomerID");
                    }
                }
                catch
                {
                    return new ArgumentException($"No ID found for the company {companyName}");
                }

                GoogleAdsClient client = new GoogleAdsClient();

                // Get the GoogleAdsService.
                GoogleAdsServiceClient googleAdsService = client.GetService(
                    Services.V4.GoogleAdsService);



                string query = "SELECT "
                                 + "campaign.id, "
                                 + "campaign.name, "
                                 + "campaign.start_date, "
                                 + "campaign.end_date, "
                                 + "metrics.impressions, "
                                 + "metrics.clicks, "
                                 + "metrics.conversions_value, "
                                 + "metrics.conversions, "
                                 + "metrics.cost_micros "
                             + "FROM campaign "
                             + "WHERE campaign.serving_status='SERVING' "
                             + "AND segments.date DURING THIS_MONTH "
                             + "ORDER BY metrics.impressions DESC";

                string lastMonthQuery = "SELECT "
                                            + "metrics.conversions_value, "
                                            + "metrics.conversions,"
                                            + "metrics.cost_micros "
                                        + "FROM campaign "
                                        + "WHERE segments.date DURING LAST_MONTH ";

                try
                {
                    RepeatedField<GoogleAdsRow> results = RequestMethods.SearchRequest(customerID, query, googleAdsService);

                    if (results.Any())
                    {
                        List<GoogleAdsCampaignDTO> campaignsDTO = new List<GoogleAdsCampaignDTO>();

                        double? totalSpend = 0;
                        double? totalValue = 0;
                        double? totalConversions = 0;
                        double? rOAS = 0;

                        for (int i = 0; i < results.Count; i++)
                        {
                            campaignsDTO.Add(new GoogleAdsCampaignDTO(results[i]));

                            totalSpend += results[i].Metrics.CostMicros;
                            totalValue += results[i].Metrics.ConversionsValue;
                            totalConversions += results[i].Metrics.Conversions;
                        }

                        double? totalSpendPounds = totalSpend / 1000000;

                        if (totalValue != 0 && totalSpendPounds != 0)
                        {
                            rOAS = totalValue / totalSpendPounds;
                        }

                        RepeatedField<GoogleAdsRow> lastMonthResults = RequestMethods.SearchRequest(customerID, lastMonthQuery, googleAdsService);

                        if (lastMonthResults.Any())
                        {
                            double? lMTotalSpend = 0;
                            double? lMTotalValue = 0;
                            double? lMTotalConversions = 0;
                            double? lMROAS = 0;

                            for(int i = 0; i < lastMonthResults.Count; i++)
                            {
                                lMTotalSpend += lastMonthResults[i].Metrics.CostMicros;
                                lMTotalValue += lastMonthResults[i].Metrics.ConversionsValue;
                                lMTotalConversions += lastMonthResults[i].Metrics.Conversions;
                            }

                            double? lMTotalSpendPounds = lMTotalSpend / 1000000;

                            if (lMTotalValue != 0 && lMTotalSpendPounds != 0)
                            {
                                lMROAS = lMTotalValue / lMTotalSpendPounds;
                            }

                            return new GoogleAdsCampaignSummaryDTO(campaignsDTO, totalSpendPounds, totalConversions, totalValue, rOAS, lMROAS);
                        }
                        else
                        {
                            return new GoogleAdsCampaignSummaryDTO(campaignsDTO, totalSpend / 1000000, totalConversions, totalValue, rOAS, 0);
                        }
                    }
                    else
                    {
                        return JsonConvert.SerializeObject("No results were found");
                    }
                }
                catch (GoogleAdsException e)
                {
                    Debug.WriteLine("Failure:");
                    Debug.WriteLine($"Message: {e.Message}");
                    Debug.WriteLine($"Failure: {e.Failure}");
                    Debug.WriteLine($"Request ID: {e.RequestId}");
                    throw;
                }
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}
