using data_handler_app.ShopifyModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace data_handler_app.ShopifyMethods
{
    public static class OrdersMethods
    {
        public static async Task<dynamic> OrdersCount(HttpClient client, string hostName, string shopifyAdminAPIVersion, string sinceDate)
        {
            try
            {
                string url = $"https://{hostName}/admin/api/{shopifyAdminAPIVersion}/orders/count.json?status=any&updated_at_min={sinceDate}";

                HttpResponseMessage response = await client.GetAsync(url);

                string responseData = await response.Content.ReadAsStringAsync();

                OrdersCountRoot ordersCountRoot = JsonConvert.DeserializeObject<OrdersCountRoot>(responseData);

                return ProcessOrdersCountResponse(ordersCountRoot.Count);
            }
            catch(Exception ex)
            {
                return ex;
            }
        }

        public static int? ProcessOrdersCountResponse(dynamic response)
        {
            int? ordersThisMonth;

            ordersThisMonth = Equals(response.GetType(), typeof(int)) ? response : null;

            return ordersThisMonth;
        }
    }
}