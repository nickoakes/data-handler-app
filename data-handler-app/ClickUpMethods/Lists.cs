using data_handler_app.ClickUpModels;
using data_handler_app.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace data_handler_app.ClickUpMethods
{
    public static class Lists
    {
        public static async Task<dynamic> GetAllLists(HttpClient client, Queue<string> folderIDs)
        {
            Queue<List> allLists = new Queue<List>();

            while(folderIDs.Count > 0)
            {
                string uri = $"{ConfigDictionary.Config()["ClickUpAPIURLRoot"]}/folder/{folderIDs.Dequeue()}/list?archived=false";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(uri);

                    string responseData = await response.Content.ReadAsStringAsync();

                    ListsRoot listsRoot = JsonConvert.DeserializeObject<ListsRoot>(responseData);

                    for (int i = 0; i < listsRoot.Lists.Count; i++)
                    {
                        allLists.Enqueue(listsRoot.Lists[i]);
                    }
                }
                catch (HttpRequestException ex)
                {
                    return ex;
                }
            }

            return allLists;
        }

        public static Queue<string> GetListIDs(Queue<List> allLists)
        {
            Queue<string> listIDs = new Queue<string>();

            while(allLists.Count > 0)
            {
                List list = allLists.Dequeue();

                if(list.TaskCount > 0)
                {
                    listIDs.Enqueue(list.Id);
                }
            }

            return listIDs;
        }

        public static async Task<Queue<string>> ReturnListIDs(HttpClient client, Queue<string> folderID)
        {
            try
            {
                Queue<List> allLists = await GetAllLists(client, folderID);

                try
                {
                    return GetListIDs(allLists);
                }
                catch
                {
                    throw;
                }
            }
            catch
            {
                return new Queue<string>();
            }
        }
    }
}