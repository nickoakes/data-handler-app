using data_handler_app.EverHourModels;
using data_handler_app.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace data_handler_app.EverHourMethods
{
    public static class Projects
    {
        public static async Task<dynamic> GetProject(string companyName, HttpClient client, Entities db)
        {
            try
            {
                string alias = AliasMethods.GetAlias(companyName, db, "Everhour");

                string uri = ConfigDictionary.Config()["EverHourURLRoot"] + $"/projects?query={alias}";

                HttpResponseMessage response = await client.GetAsync(uri);

                string responseData = await response.Content.ReadAsStringAsync();

                ProjectRoot projectRoot = JsonConvert.DeserializeObject<List<ProjectRoot>>(responseData).FirstOrDefault();

                return new ProjectDTO(projectRoot);
            }
            catch(Exception ex)
            {
                return ex;
            }
        }
    }
}