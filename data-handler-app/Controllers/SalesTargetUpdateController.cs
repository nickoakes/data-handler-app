using data_handler_app.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using SendGrid.Helpers.Mail;
using System.Net.Http;

namespace data_handler_app.Controllers
{
    public class SalesTargetUpdateController : ApiController
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

        public async Task<dynamic> Get()
        {
            if (CheckClientSecret())
            {
                Entities db = new Entities();

                Dictionary<string, string> emailConfig = ConfigDictionary.EmailConfig();

                var storesRequiringUpdate = new List<StoreCustomData>();

                foreach(StoreCustomData store in db.StoreCustomDatas)
                {
                    DateTime lastUpdate = db.SalesTargetHistories
                                            .Where(x => x.StoreID == store.StoreID)
                                            .OrderByDescending(x => x.DateUpdated)
                                            .FirstOrDefault()
                                            .DateUpdated;

                    if((DateTime.Now - lastUpdate).TotalDays > 30)
                    {
                        storesRequiringUpdate.Add(store);
                    }
                }

                if (storesRequiringUpdate.Any())
                {
                    string storeNames = string.Empty;

                    foreach (StoreCustomData store in storesRequiringUpdate)
                    {
                        storeNames += $"<li><b>{ store.StoreName }</b> - Current Sales Target: £{ store.SalesTarget }</li>";
                    }

                    string messageBody = "<html><body>" +
                                         $"<span style='display:inline-block;'><img src='https://cdn.shopify.com/s/files/applications/75204543d0fec5bbf9e7f1db609e8804_512x512.png?1594118407' style='width:50%;'/><h2>Sales Target Update Alert</h2></span>" +
                                         $"<p>It has been more than 30 days since Sales Targets were updated for the following stores:</p><br/>" +
                                         $"<ul>{ storeNames }</ul>" +
                                         "</body></html>";

                    try
                    {
                        await EmailAlert.SendEmailAlert(new EmailAddress(emailConfig["SalesTargetRecipientAddress"]), "Sales Target Update Alert", messageBody);

                        return "Email sent!";
                    } 
                    catch(Exception exception)
                    {
                        Console.WriteLine(exception.ToString());

                        return "Email failed to send.";
                    }
                }
                else
                {
                    return "No updates required.";
                }
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}
