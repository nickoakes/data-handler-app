using data_handler_app.BugHerdMethods;
using data_handler_app.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace data_handler_app.Controllers
{
    public class BugHerdController : ApiController
    {
        Projects projectsMethods = new Projects();

        Tasks tasksMethods = new Tasks();

        Entities db = new Entities();
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
                using(HttpClient client = new HttpClient())
                {
                    string username = ConfigDictionary.Config()["BugHerdAPIKey"];

                    string password = ConfigDictionary.Config()["BugHerdAPIPassword"];

                    byte[] authToken = Encoding.ASCII.GetBytes($"{username}:{password}");

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                    string projectID = await projectsMethods.GetProjectID(companyName, client, db);

                    if (!string.IsNullOrEmpty(projectID))
                    {
                        try
                        {
                            return await tasksMethods.GetAllTasks(client, projectID);
                        }
                        catch (Exception ex)
                        {
                            return ex;
                        }
                    }
                    else
                    {
                        return new ArgumentException($"No projects found for the company: {companyName}");
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
