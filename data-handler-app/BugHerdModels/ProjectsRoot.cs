using Newtonsoft.Json;
using System.Collections.Generic;

namespace data_handler_app.BugHerdModels
{
    public class ProjectsRoot
    {
        [JsonProperty("projects")]
        public List<Project> Projects { get; set; }
    }

    public partial class Project
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("has_custom_columns")]
        public bool HasCustomCollections { get; set; }
    }
}