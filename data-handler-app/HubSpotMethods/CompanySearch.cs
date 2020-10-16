using data_handler_app.HubSpotModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace data_handler_app.HubSpotMethods
{
    public static class CompanySearch
    {
        public static async Task<dynamic> GetCompany(HttpClient client, string companyName, Entities db)
        {
            //string alias;

            //try
            //{
            //    alias = AliasMethods.GetAlias(companyName, db, "HubSpot");
            //}
            //catch
            //{
            //    return new ArgumentException($"No HubSpot alias found for the company name {companyName}");
            //}

            //List<Filter> filters = new List<Filter>();

            //Filter nameFilter = new Filter("name", "EQ", alias);

            //filters.Add(nameFilter);

            //string[] searchProps = new string[] { "domain", "monthly_retainer", "contract_start_date", "monthly_ad_budget", "notes_last_contacted", "num_contacted_notes", "hs_lastmodifieddate" };

            //RequestBody search = new RequestBody(filters, searchProps.ToList());

            //string searchJson = JsonConvert.SerializeObject(search);

            //StringContent searchData = new StringContent(searchJson, Encoding.UTF8, "application/json");

            //string uri = $"{ConfigDictionary.Config()["HubSpotAPIURLRoot"]}/companies/search?hapikey={ConfigDictionary.Config()["HubSpotAPIKey"]}";

            //try
            //{
            //    HttpResponseMessage response = await client.PostAsync(uri, searchData);

            //    string responseData = await response.Content.ReadAsStringAsync();

            //    CompanyRoot.Company companyData = JsonConvert.DeserializeObject<CompanyRoot.Company>(responseData);

            //    return companyData;
            //}
            //catch (HttpRequestException ex)
            //{
            //    return ex;
            //}

            string[] searchProps = new string[] { "domain", "monthly_retainer", "contract_start_date", "monthly_ad_budget", "notes_last_contacted", "num_contacted_notes", "hs_lastmodifieddate" };

            try
            {
                return await HubSpotShared.GetHubSpotInfo(client, db, companyName, searchProps);
            } 
            catch(Exception ex)
            {
                return ex;
            }
        }

        public static CompanyDTO CreateCompanyDTO(CompanyRoot.Result company)
        {
            return new CompanyDTO(company.Properties.Domain,
                                  company.Properties.ContractStartDate,
                                  company.Properties.MonthlyRetainer,
                                  company.Properties.MonthlyAdBudget,
                                  company.Properties.NotesLastContacted,
                                  company.Properties.NumContactedNotes,
                                  company.Properties.HsLastmodifieddate);
        }
    }
}