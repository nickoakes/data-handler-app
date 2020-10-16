using data_handler_app.ShopifyModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace data_handler_app.ShopifyMethods
{
    public static class BulkAction
    {
        public static async Task<dynamic> PerformBulkAction(HttpClient client, string companyName, Entities db, string shopifyAdminAPIVersion)
        {
            try
            {
                //Check Shopify credentials exist in database
                if (db.StoreCustomDatas.Any(x => x.StoreName == companyName) &&
                    db.StoreCustomDatas.FirstOrDefault(x => x.StoreName == companyName).ShopifyID != null)
                {
                    StoreCustomData store = db.StoreCustomDatas.FirstOrDefault(x => x.StoreName == companyName);

                    ShopifyCredential credentials = db
                                                    .ShopifyCredentials
                                                    .Where(x => x.ID == store.ShopifyID)
                                                    .FirstOrDefault();

                    //Create request
                    client.DefaultRequestHeaders.Add("X-Shopify-Access-Token", credentials.Password);

                    string url = $"https://{credentials.HostName}/admin/api/{shopifyAdminAPIVersion}/graphql.json";

                    StringContent query;

                    if(db.SalesDatas.Any(x => x.StoreID == store.StoreID))
                    {
                        //Some sales data for store already in database
                        DateTimeOffset mostRecent = db.LastShopifyDataUpdates.FirstOrDefault(x => x.StoreID == store.StoreID).LastUpdate;

                        //query = new StringContent($"mutation{{ bulkOperationRunQuery ( query: \"\"\" {{orders(query:\"updated_at:>'{mostRecent:yyyy-MM-ddTHH:mm:ssZ}'\") {{pageInfo {{hasNextPage}}edges {{node {{createdAt updatedAt totalPriceSet {{shopMoney {{amount}}}} refunds {{totalRefundedSet {{shopMoney {{amount}}}}}}}}cursor}}}}}}\"\"\"){{bulkOperation {{ id status}}}}}}", Encoding.UTF8, "application/graphql");
                        query = new StringContent($"mutation{{ bulkOperationRunQuery ( query: \"\"\" {{tenderTransactions(query:\"processed_at:>'{mostRecent:yyyy-MM-ddTHH:mm:ssZ}'\") {{ edges {{node {{ processedAt amount {{amount}}}}}}}}}}\"\"\"){{bulkOperation {{ id status}}}}}}", Encoding.UTF8, "application/graphql");
                    }
                    else
                    {
                        //'New' store with no sales data in database
                        //query = new StringContent($"mutation{{ bulkOperationRunQuery ( query: \"\"\" {{orders {{pageInfo {{hasNextPage}}edges {{node {{createdAt updatedAt displayFinancialStatus totalPriceSet {{shopMoney {{amount}}}} refunds {{totalRefundedSet {{shopMoney {{amount}}}}}}}}cursor}}}}}}\"\"\"){{bulkOperation {{ id status}}}}}}", Encoding.UTF8, "application/graphql");
                        query = new StringContent($"mutation{{ bulkOperationRunQuery ( query: \"\"\" {{tenderTransactions {{ edges {{ node {{ processedAt amount {{amount}}}}}}}}}}\"\"\"){{bulkOperation {{ id status}}}}}}", Encoding.UTF8, "application/graphql");
                    }

                    HttpResponseMessage response = await client.PostAsync(url, query);

                    Debug.WriteLine($"{DateTime.Now} - Response received for Graph QL query on {companyName}...");

                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Polling...");

                        dynamic downloadURL = await PollBulkAction(client, url, db, shopifyAdminAPIVersion);

                        //Await return of JSONL file download URL in polling response
                        while(downloadURL.GetType() != typeof(string))
                        {
                            downloadURL = await PollBulkAction(client, url, db, shopifyAdminAPIVersion);
                        }

                        if (!string.IsNullOrEmpty(downloadURL))
                        {
                            return DownloadJSONL(downloadURL, companyName);
                        }
                        else
                        {
                            return db.SalesDatas.Where(x => x.StoreID == store.StoreID).OrderByDescending(x => x.Date);
                        }
                    }
                    else
                    {
                        return new ArgumentException("Request to create bulk operation failed");
                    }
                }
                else
                {
                    return new ArgumentException($"No store was found with the name {companyName}");
                }
            } 
            catch(Exception ex)
            {
                return ex;
            }
        }

        public static async Task<dynamic> PollBulkAction(HttpClient client, string url, Entities db, string shopifyAdminAPIVersion)
        {
            try
            {
                await Task.Delay(1000);

                StringContent query = new StringContent("query { currentBulkOperation { id status errorCode createdAt completedAt objectCount fileSize url partialDataUrl }}", Encoding.UTF8, "application/graphql");

                HttpResponseMessage response = await client.PostAsync(url, query);

                string responseData = await response.Content.ReadAsStringAsync();

                BulkOperationStatusRoot ordersRoot = JsonConvert.DeserializeObject<BulkOperationStatusRoot>(responseData);

                string downloadURL;

                if (string.Equals(ordersRoot.Data.CurrentBulkOperation.Status, "COMPLETED") && !string.IsNullOrEmpty(ordersRoot.Data.CurrentBulkOperation.Url))
                {
                    downloadURL = ordersRoot.Data.CurrentBulkOperation.Url.Replace("{", "").Replace("}", "");

                    return downloadURL;
                }
                else if(string.Equals(ordersRoot.Data.CurrentBulkOperation.Status, "COMPLETED") && string.IsNullOrEmpty(ordersRoot.Data.CurrentBulkOperation.Url))
                {
                    return "";
                }

                return await PollBulkAction(client, url, db, shopifyAdminAPIVersion);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static dynamic DownloadJSONL(string url, string companyName)
        {
            Debug.WriteLine("Polling complete...");
            try
            {
                using (WebClient client = new WebClient())
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath($"~/orders-data"));

                    //Create temporary JSONL file path
                    string path = HttpContext.Current.Server.MapPath($"~/orders-data/{companyName.Replace(" ", "-")}-order-data.jsonl");

                    client.DownloadFile(url, path);

                    Debug.WriteLine("JSONL file downloaded...");

                    return path;
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static dynamic WriteSalesData(string path, int storeID, Entities db, DateTime operationStart)
        {
            Debug.WriteLine("Writing to database...");

            string line;

            try
            {
                StreamReader file = File.OpenText(path);

                List<SalesDataDTO> allSales = new List<SalesDataDTO>();

                while ((line = file.ReadLine()) != null)
                {
                    OrderJSONLRoot order = JsonConvert.DeserializeObject<OrderJSONLRoot>(line);

                    DateTime dateCreated = new DateTime(order.ProcessedAt.Year, order.ProcessedAt.Month, 1);

                    if(allSales.Any(x => x.Date == dateCreated))
                    {
                        allSales.FirstOrDefault(x => x.Date == dateCreated).Sales += order.Amount.AmountValue;
                    }
                    else
                    {
                        allSales.Add(new SalesDataDTO(dateCreated, storeID, order.Amount.AmountValue));
                    }
                }

                foreach (SalesDataDTO salesData in allSales)
                {
                    if (!db.SalesDatas.Any(x => x.StoreID == storeID && x.Date == salesData.Date))
                    {
                        SalesData sales = new SalesData();

                        sales.ID = salesData.ID;
                        sales.StoreID = salesData.StoreID;
                        sales.Date = salesData.Date;
                        sales.Sales = salesData.Sales.ToString();

                        db.SalesDatas.Add(sales);
                    }
                    else
                    {
                        decimal newSales = decimal.Parse(db.SalesDatas.FirstOrDefault(x => x.StoreID == storeID && x.Date == salesData.Date).Sales) + salesData.Sales;

                        db.SalesDatas.FirstOrDefault(x => x.StoreID == storeID && x.Date == salesData.Date).Sales = newSales.ToString();
                    }
                }

                if (db.LastShopifyDataUpdates.Any(x => x.StoreID == storeID))
                {
                    db.LastShopifyDataUpdates.FirstOrDefault(x => x.StoreID == storeID).LastUpdate = operationStart;
                }
                else
                {
                    LastShopifyDataUpdate update = new LastShopifyDataUpdate();

                    update.ID = Guid.NewGuid();
                    update.StoreID = storeID;
                    update.LastUpdate = operationStart;

                    db.LastShopifyDataUpdates.Add(update);
                }

                db.SaveChanges();

                file.Dispose();

                File.Delete(path);

                Debug.WriteLine("Complete!");

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return ex;
            }
        }
    }
}