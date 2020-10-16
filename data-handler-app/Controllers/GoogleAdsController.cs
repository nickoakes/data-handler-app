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
using data_handler_app.GoogleAdsModels;
using data_handler_app.GoogleAdsMethods;

namespace data_handler_app.Controllers
{
    public class GoogleAdsController : ApiController
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
                GoogleAdsServiceClient googleAdsService = client.GetService(Services.V4.GoogleAdsService);

                string query = "SELECT "
                                + "account_budget.approved_spending_limit_micros, "
                                + "account_budget.proposed_spending_limit_micros, "
                                + "account_budget.approved_start_date_time, "
                                + "account_budget.proposed_start_date_time, "
                                + "account_budget.approved_end_date_time, "
                                + "account_budget.proposed_end_date_time, "
                                + "account_budget.amount_served_micros "
                                + "FROM account_budget";

                try
                {
                    RepeatedField<GoogleAdsRow> results = RequestMethods.SearchRequest(customerID, query, googleAdsService);

                    if (results.Any())
                    {
                        List<GoogleAdsBudgetDTO> resultsDTO = new List<GoogleAdsBudgetDTO>();

                        for(int i = 0; i < results.Count; i++)
                        {
                            resultsDTO.Add(new GoogleAdsBudgetDTO(results[i]));
                        }

                        return resultsDTO;
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
