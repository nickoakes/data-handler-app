using data_handler_app.FacebookAdsMethods;
using data_handler_app.FacebookAdsModels;
using data_handler_app.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace data_handler_app.Controllers
{
    public class FacebookAdsController : ApiController
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

        public async Task<dynamic> Get(string companyName)
        {
            if (CheckClientSecret())
            {
                using (HttpClient client = new HttpClient())
                using (Entities db = new Entities())
                {
                    try
                    {
                        AllCampaignsRoot allCampaignsRoot = await CampaignsMethods.GetAllCampaigns(companyName, db, client);

                        return await InsightsMethods.ReturnFacebookAdsDTO(allCampaignsRoot, client);
                    }
                    catch (Exception ex)
                    {
                        return ex;
                    }
                }
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
    }
}
