using data_handler_app.BugHerdModels;
using data_handler_app.Shared;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace data_handler_app.BugHerdMethods
{
    public class Projects
    {
        public async Task<dynamic> GetProjectID(string companyName, HttpClient client, Entities db)
        {
            try
            {
                string alias = AliasMethods.GetAlias(companyName, db, "BugHerd");

                string uri = $"{ConfigDictionary.Config()["BugHerdURLRoot"]}/projects/active.json";

                HttpResponseMessage response = await client.GetAsync(uri);

                string responseData = await response.Content.ReadAsStringAsync();

                ProjectsRoot allProjects = JsonConvert.DeserializeObject<ProjectsRoot>(responseData);

                return allProjects.Projects.FirstOrDefault(x => x.Name == alias).ID;
            }
            catch
            {
                return null;
            }
        }
    }
}