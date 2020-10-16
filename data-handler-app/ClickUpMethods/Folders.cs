using data_handler_app.ClickUpModels;
using data_handler_app.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace data_handler_app.ClickUpMethods
{
    public static class Folders
    {
        public static async Task<dynamic> GetAllFolders(HttpClient client)
        {
            Queue<string> uris = new Queue<string>(2);

            List<Folder> allFolders = new List<Folder>();

            string clickUpAPIURLRoot = ConfigDictionary.Config()["ClickUpAPIURLRoot"];

            uris.Enqueue($"{clickUpAPIURLRoot}/space/{ConfigDictionary.Config()["ClickUpProjectsSpaceID"]}/folder?archived=false");

            uris.Enqueue($"{clickUpAPIURLRoot}/space/{ConfigDictionary.Config()["ClickUpRetainersSpaceID"]}/folder?archived=false");

            try
            {
                while(uris.Count > 0)
                {
                    HttpResponseMessage response = await client.GetAsync(uris.Dequeue());

                    string responseData = await response.Content.ReadAsStringAsync();

                    FoldersRoot folders = JsonConvert.DeserializeObject<FoldersRoot>(responseData);

                    for(int i = 0; i < folders.Folders.Count; i++)
                    {
                        allFolders.Add(folders.Folders[i]);
                    }
                }

                return allFolders;
            }
            catch (HttpRequestException ex)
            {
                return ex;
            }
        }

        public static Queue<string> GetFolderID(List<Folder> allfolders, string alias)
        {
            if(allfolders.Any(x => x.Name == alias))
            {
                Queue<string> folderIDs = new Queue<string>();

                for(int i = 0; i < allfolders.Count; i++)
                {
                    if(allfolders[i].Name == alias)
                    {
                        folderIDs.Enqueue(allfolders[i].Id);
                    }
                }

                return folderIDs;
            }
            else
            {
                return null;
            }
        }

        public static async Task<Queue<string>> ReturnFolderID(HttpClient client, string companyName, Entities db)
        {
            try
            {
                string alias = AliasMethods.GetAlias(companyName, db, "ClickUp");

                List<Folder> allFolders = await GetAllFolders(client);

                try
                {
                    return GetFolderID(allFolders, alias);
                }
                catch
                {
                    throw;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}