using data_handler_app.Shared;
using data_handler_app.ShopifyMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace data_handler_app.Controllers
{
    public class ShopifyController : ApiController
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
                DateTime operationStarted = DateTime.Now;

                Entities db = new Entities();

                using (HttpClient client = new HttpClient())
                {
                    var result = await BulkAction.PerformBulkAction(client, companyName, db, ConfigDictionary.Config()["ShopifyAdminAPIVersion"]);

                    if (Equals(result.GetType(), typeof(string)))
                    {
                        StoreCustomData store = db.StoreCustomDatas.FirstOrDefault(x => x.StoreName == companyName);

                        var dbResult = BulkAction.WriteSalesData(result,
                                                                  store.StoreID,
                                                                  db,
                                                                  operationStarted);

                        if (!Equals(dbResult.GetType(), typeof(Exception)))
                        {
                            return db.SalesDatas.Where(x => x.StoreID == store.StoreID).OrderBy(x => x.Date);
                        }
                        else
                        {
                            return new ArgumentException("An error occurred while writing to the database.");
                        }
                    }
                    else
                    {
                        return result;
                    }
                }
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}
