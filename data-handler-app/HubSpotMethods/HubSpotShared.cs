using data_handler_app.HubSpotModels;
using data_handler_app.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace data_handler_app.HubSpotMethods
{
    public class HubSpotShared
    {
        public static async Task<dynamic> GetHubSpotInfo(HttpClient client, Entities db, string companyName, string[] searchProps)
        {
            string alias;

            try
            {
                alias = AliasMethods.GetAlias(companyName, db, "HubSpot");
            }
            catch
            {
                return new ArgumentException($"No HubSpot alias found for the company name {companyName}");
            }

            List<Filter> filters = new List<Filter>();

            Filter nameFilter = new Filter("name", "EQ", alias);

            filters.Add(nameFilter);

            RequestBody search = new RequestBody(filters, searchProps.ToList());

            string searchJson = JsonConvert.SerializeObject(search);

            StringContent searchData = new StringContent(searchJson, Encoding.UTF8, "application/json");

            string uri = $"{ConfigDictionary.Config()["HubSpotAPIURLRoot"]}/companies/search?hapikey={ConfigDictionary.Config()["HubSpotAPIKey"]}";

            try
            {
                HttpResponseMessage response = await client.PostAsync(uri, searchData);

                string responseData = await response.Content.ReadAsStringAsync();

                CompanyRoot.Company companyData = JsonConvert.DeserializeObject<CompanyRoot.Company>(responseData);

                return companyData;
            }
            catch (HttpRequestException ex)
            {
                return ex;
            }
        }
    }
}