using data_handler_app.BugHerdModels;
using data_handler_app.Shared;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace data_handler_app.BugHerdMethods
{
    public class Tasks
    {
        public async Task<dynamic> GetAllTasks(HttpClient client, string projectID)
        {
            string url = ConfigDictionary.Config()["BugHerdURLRoot"] + $"/projects/{projectID}/tasks.json";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                string responseData = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TasksRoot>(responseData);
            } 
            catch
            {
                return null;
            }
        }
    }
}