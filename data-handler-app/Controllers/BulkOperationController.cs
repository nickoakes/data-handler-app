using data_handler_app.Shared;
using data_handler_app.ShopifyMethods;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace data_handler_app.Controllers
{
    public class BulkOperationController : ApiController
    {
        public async Task<dynamic> Get(string companyName)
        {
            DateTime operationStarted = DateTime.Now;

            using (HttpClient client = new HttpClient())
            using(Entities db = new Entities())
            {
                var result = await BulkAction.PerformBulkAction(client, companyName, db, ConfigDictionary.Config()["ShopifyAdminAPIVersion"]);

                if (Equals(result.GetType(), typeof(string)))
                {
                    return BulkAction.WriteSalesData(result,
                                                      db
                                                          .StoreCustomDatas
                                                          .Where(x => x.StoreName == companyName)
                                                          .FirstOrDefault()
                                                          .StoreID,
                                                      db,
                                                      operationStarted);
                }
                else
                {
                    return result;
                }
            }
        }
    }
}
