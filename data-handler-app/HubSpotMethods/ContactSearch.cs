using data_handler_app.HubSpotModels;
using data_handler_app.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace data_handler_app.HubSpotMethods
{
    public static class ContactSearch
    {
        public static async Task<dynamic> GetContacts(HttpClient client, CompanyRoot.Result company)
        {
            List<Filter> filters = new List<Filter>();

            Filter nameFilter = new Filter("associatedcompanyid", "EQ", company.Id);

            filters.Add(nameFilter);

            string[] searchProps = new string[] { "firstname", "lastname", "email", "phone" };

            RequestBody search = new RequestBody(filters, searchProps.ToList());

            string searchJson = JsonConvert.SerializeObject(search);

            StringContent searchData = new StringContent(searchJson, Encoding.UTF8, "application/json");

            string uri = $"{ConfigDictionary.Config()["HubSpotAPIURLRoot"]}/contacts/search?hapikey={ConfigDictionary.Config()["HubSpotAPIKey"]}";

            try
            {
                HttpResponseMessage response = await client.PostAsync(uri, searchData);

                string responseData = await response.Content.ReadAsStringAsync();

                ContactRoot.Contact contactsData = JsonConvert.DeserializeObject<ContactRoot.Contact>(responseData);

                return contactsData;
            }
            catch (HttpRequestException ex)
            {
                return ex;
            }
        }

        public static List<ContactDTO> CreateContactDTOs(ContactRoot.Contact contact)
        {
            List<ContactDTO> contactDTOs = new List<ContactDTO>();

            for(int i = 0; i < contact.Results.Count; i++)
            {
                contactDTOs.Add(new ContactDTO($"{contact.Results[i].Properties.Firstname} {contact.Results[i].Properties.Lastname}", contact.Results[i].Properties.Email, contact.Results[i].Properties.Phone));
            }

            return contactDTOs;
        }
    }
}