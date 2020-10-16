using data_handler_app.HubSpotMethods;
using data_handler_app.HubSpotModels;
using data_handler_app.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace data_handler_app.Controllers
{
    public class HubSpotContactsController : ApiController
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

        [HttpGet]
        public async Task<dynamic> Get(string companyName)
        {
            if (CheckClientSecret())
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    using (Entities db = new Entities())
                    {
                        CompanyRoot.Company company = await CompanySearch.GetCompany(client, companyName, db);

                        ContactRoot.Contact contact = await ContactSearch.GetContacts(client, company.Results.FirstOrDefault());

                        return ContactSearch.CreateContactDTOs(contact);
                    }
                }
                catch (Exception ex)
                {
                    return ex;
                }
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}
