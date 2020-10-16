using data_handler_app.ClickUpModels;
using data_handler_app.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace data_handler_app.ClickUpMethods
{
    public static class Tasks
    {
        public static async Task<dynamic> GetAllTasks(HttpClient client, Queue<string> listIDs)
        {
            List<TaskDTO> allTasks = new List<TaskDTO>();

            try
            {
                while (listIDs.Count > 0)
                {
                    string uri = $"{ConfigDictionary.Config()["ClickUpAPIURLRoot"]}/list/{listIDs.Dequeue()}/task?archived=false";

                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(uri);

                        string responseData = await response.Content.ReadAsStringAsync();

                        TasksRoot tasks = JsonConvert.DeserializeObject<TasksRoot>(responseData);

                        for (int i = 0; i < tasks.Tasks.Count; i++)
                        {
                            allTasks.Add(new TaskDTO(tasks.Tasks[i]));
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }

                return allTasks;
            } 
            catch(Exception ex)
            {
                return ex;
            }
        }
    }
}