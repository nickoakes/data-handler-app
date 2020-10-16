using data_handler_app.Shared;
using data_handler_app.ShopifyMethods;
using data_handler_app.ShopifyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace data_handler_app.Controllers
{
    public class ShopifyOrdersCountController : ApiController
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
                    if (db.StoreCustomDatas.Any(x => x.StoreName == companyName) &&
                        db.StoreCustomDatas.FirstOrDefault(x => x.StoreName == companyName).ShopifyID != null)
                    {
                        StoreCustomData store = db.StoreCustomDatas.FirstOrDefault(x => x.StoreName == companyName);

                        ShopifyCredential credentials = db
                                                        .ShopifyCredentials
                                                        .Where(x => x.ID == store.ShopifyID)
                                                        .FirstOrDefault();

                        client.DefaultRequestHeaders.Add("X-Shopify-Access-Token", credentials.Password);

                        string shopifyAPIVersion = ConfigDictionary.Config()["ShopifyAdminAPIVersion"];

                        DateTimeOffset thisMonth = new DateTimeOffset(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1, 0, 0, 0, TimeSpan.Zero);

                        DateTimeOffset lastMonth = thisMonth.AddMonths(-1);

                        DateTimeOffset monthBeforeLast = thisMonth.AddMonths(-2);

                        try
                        {
                            int? ordersThisMonth = await OrdersMethods.OrdersCount(client, credentials.HostName, shopifyAPIVersion, thisMonth.ToString("yyyy-MM-ddTHH:mm:ss zzz"));

                            int? ordersLastMonth = await OrdersMethods.OrdersCount(client, credentials.HostName, shopifyAPIVersion, lastMonth.ToString("yyyy-MM-ddTHH:mm:ss zzz"));

                            int? ordersMonthBeforeLast = await OrdersMethods.OrdersCount(client, credentials.HostName, shopifyAPIVersion, monthBeforeLast.ToString("yyyy-MM-ddTHH:mm:ss zzz"));

                            return new OrdersCountDTO(ordersThisMonth, ordersLastMonth, ordersMonthBeforeLast);
                        }
                        catch (Exception ex)
                        {
                            return ex;
                        }
                    }
                    else
                    {
                        return new ArgumentException($"No store was found with the name {companyName}");
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
