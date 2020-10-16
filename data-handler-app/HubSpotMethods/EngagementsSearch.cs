using data_handler_app.HubSpotModels;
using data_handler_app.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace data_handler_app.HubSpotMethods
{
    public static class EngagementsSearch
    {
        public static async Task<dynamic> GetEngagements(HttpClient client, string companyName, Entities db, string companyID)
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

            string uri = $"https://api.hubapi.com/engagements/v1/engagements/associated/COMPANY/{companyID}/paged?limit=100&hapikey=" + ConfigDictionary.Config()["HubSpotAPIKey"];

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);

                string responseData = await response.Content.ReadAsStringAsync();

                EngagementsRoot engagementsData = JsonConvert.DeserializeObject<EngagementsRoot>(responseData);

                while (engagementsData.HasMore)
                {
                    engagementsData = await ChainedEngagementsRequest(engagementsData, companyID, client);
                }

                return engagementsData;
            }
            catch (HttpRequestException ex)
            {
                return ex;
            }
        }

        public static async Task<EngagementsRoot> ChainedEngagementsRequest(EngagementsRoot engagementsData, string companyID, HttpClient client)
        {
            string uri = $"https://api.hubapi.com/engagements/v1/engagements/associated/COMPANY/{companyID}/paged?limit=100&offset={engagementsData.Offset}&hapikey=" + ConfigDictionary.Config()["HubSpotAPIKey"];

            HttpResponseMessage response = await client.GetAsync(uri);

            string responseData = await response.Content.ReadAsStringAsync();

            engagementsData = JsonConvert.DeserializeObject<EngagementsRoot>(responseData);

            return engagementsData;
        }

        public static List<EngagementsContact> CreateContactList(List<From> rawData)
        {
            List<EngagementsContact> contacts = new List<EngagementsContact>();

            string names = "";
            string emails = "";
            if (rawData.Any())
            {
                for (int i = 0; i < rawData.Count; i++)
                {
                    if(rawData[i].Email != rawData.Last().Email)
                    {
                        names += $"{rawData[i].FirstName} {rawData[i].LastName}, ";
                        emails += $"{rawData[i].Email}, ";
                    }
                    else
                    {
                        names += $"{rawData[i].FirstName} {rawData[i].LastName}";
                        emails += $"{rawData[i].Email}";
                    }
                }

                contacts.Add(new EngagementsContact(names, emails));

            }
            else
            {
                contacts.Add(new EngagementsContact(string.Empty, string.Empty));
            }

            return contacts;
        }
    }
}