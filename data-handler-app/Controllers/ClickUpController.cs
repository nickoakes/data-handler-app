using data_handler_app.ClickUpMethods;
using data_handler_app.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace data_handler_app.Controllers
{
    public class ClickUpController : ApiController
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
                using(Entities db = new Entities())
                {
                    client.DefaultRequestHeaders.Add("Authorization", ConfigDictionary.Config()["ClickUpAPIKey"]);

                    Queue<string> folderIDs = await Folders.ReturnFolderID(client, companyName, db);

                    if (folderIDs.Any())
                    {
                        Queue<string> listIDs = await Lists.ReturnListIDs(client, folderIDs);

                        if (listIDs.Any())
                        {
                            return await Tasks.GetAllTasks(client, listIDs);
                        }
                        else
                        {
                            return new ArgumentException("No lists found");
                        }
                    }
                    else
                    {
                        return new ArgumentException($"No folders found for the company: {companyName}");
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
