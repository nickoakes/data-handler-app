using Google.Ads.GoogleAds;
using Google.Ads.GoogleAds.Lib;
using Google.Ads.GoogleAds.V4.Errors;
using Google.Ads.GoogleAds.V4.Services;
using Google.Protobuf.Collections;
using data_handler_app.GoogleAdsModels;
using data_handler_app.Shared;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace data_handler_app.GoogleAdsMethods
{
    public static class DailyImpressions
    {
        public async static Task<dynamic> ImpressionsYesterday(List<string> companyNames)
        {
            var dailyImpressionsDTO = new DailyImpressionsDTO();

            dailyImpressionsDTO.DailyImpressionsSummary = new List<CompanyDailyImpressionsSummary>();

            foreach(string company in companyNames)
            {
                string customerID;

                try
                {
                    using (Entities db = new Entities())
                    {
                        customerID = AliasMethods.GetAlias(company, db, "GoogleAdsCustomerID");
                    }
                }
                catch
                {
                    return new ArgumentException($"No ID found for the company { company }");
                }

                try
                {
                    GoogleAdsClient client = new GoogleAdsClient();

                    // Get the GoogleAdsService.
                    GoogleAdsServiceClient googleAdsService = client.GetService(
                        Services.V4.GoogleAdsService);

                    string dailyImpressionsQuery = "SELECT "
                                                    + "campaign.id, "
                                                    + "campaign.name, "
                                                    + "metrics.impressions "
                                                + "FROM campaign "
                                                + "WHERE campaign.serving_status='SERVING' "
                                                + "AND segments.date DURING YESTERDAY";

                    RepeatedField<GoogleAdsRow> dailyImpressionsResults = RequestMethods.SearchRequest(customerID, dailyImpressionsQuery, googleAdsService);

                    if(dailyImpressionsResults.Any(x => x.Metrics.Impressions > 0 && x.Metrics.Impressions < 100))
                    {
                        dailyImpressionsDTO.DailyImpressionsSummary.Add(new CompanyDailyImpressionsSummary(company, dailyImpressionsResults));
                    }
                }
                catch (GoogleAdsException e)
                {
                    Debug.WriteLine("Failure:");
                    Debug.WriteLine($"Message: {e.Message}");
                    Debug.WriteLine($"Failure: {e.Failure}");
                    Debug.WriteLine($"Request ID: {e.RequestId}");
                }
            }

            return await SendEmailAlert(dailyImpressionsDTO);
        }

        public async static Task<string> SendEmailAlert(DailyImpressionsDTO dailyImpressionsSummary)
        {
            Dictionary<string, string> emailConfig = ConfigDictionary.EmailConfig();

            string companies = string.Empty;

            foreach (CompanyDailyImpressionsSummary company in dailyImpressionsSummary.DailyImpressionsSummary)
            {
                string failingCampaigns = string.Empty;

                foreach (CampaignDailyImpressionsSummary campaign in company.Campaigns)
                {
                    failingCampaigns += $"<li>{ campaign.Name }: <span style='color:red;font-weight:bold;'>{ campaign.Impressions }</span> impressions</li>";
                }

                companies += $"<li><b>{ company.Name }</b>: <ul>{ failingCampaigns }</ul></li>";
            }

            string messageBody = "<html><body>" +
                                $"<span style='display:inline-block;'><img src='https://cdn.shopify.com/s/files/applications/75204543d0fec5bbf9e7f1db609e8804_512x512.png?1594118407' style='width:50%;'/><h2>Google Ads Campaign Alert</h2></span>" +
                                $"<p>The following campaigns achieved fewer than 100 impressions yesterday:</p><br/>" +
                                $"<ul>{ companies }</ul>" +
                                "</body></html>";

            try
            {
                await EmailAlert.SendEmailAlert(new EmailAddress(emailConfig["GoogleAdsRecipientAddress"]),
                                                $"Google Ads Campaign Alert",
                                                messageBody);

                return "Email sent!";
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());

                return "Email failed to send.";
            }
        }
    }
}