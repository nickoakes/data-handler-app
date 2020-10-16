using Newtonsoft.Json;
using System.Collections.Generic;

namespace data_handler_app.ClickUpModels
{
    public partial class FoldersRoot
    {
        [JsonProperty("folders")]
        public List<Folder> Folders { get; set; }
    }

    public partial class Folder
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("orderindex")]
        public int? Orderindex { get; set; }

        [JsonProperty("override_statuses")]
        public bool? OverrideStatuses { get; set; }

        [JsonProperty("hidden")]
        public bool? Hidden { get; set; }

        [JsonProperty("space")]
        public FolderSpace Space { get; set; }

        [JsonProperty("task_count")]
        public int? TaskCount { get; set; }

        [JsonProperty("archived")]
        public bool? Archived { get; set; }

        [JsonProperty("statuses")]
        public List<object> Statuses { get; set; }

        [JsonProperty("lists")]
        public List<FoldersList> Lists { get; set; }

        [JsonProperty("permission_level")]
        public string PermissionLevel { get; set; }
    }

    public partial class FoldersList
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("orderindex")]
        public int? Orderindex { get; set; }

        [JsonProperty("status")]
        public FoldersPurpleStatus Status { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("assignee")]
        public User Assignee { get; set; }

        [JsonProperty("task_count")]
        public int? TaskCount { get; set; }

        [JsonProperty("due_date")]
        public int? DueDate { get; set; }

        [JsonProperty("start_date")]
        public int? StartDate { get; set; }

        [JsonProperty("space")]
        public ListSpace Space { get; set; }

        [JsonProperty("archived")]
        public bool? Archived { get; set; }

        [JsonProperty("override_statuses")]
        public bool? OverrideStatuses { get; set; }

        [JsonProperty("statuses")]
        public List<FoldersStatusElement> Statuses { get; set; }

        [JsonProperty("permission_level")]
        public string PermissionLevel { get; set; }
    }

    public partial class ListSpace
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("access")]
        public bool? Access { get; set; }
    }

    public partial class FoldersPurpleStatus
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("hide_label")]
        public bool? HideLabel { get; set; }
    }

    public partial class FoldersStatusElement
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("orderindex")]
        public int? Orderindex { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class FolderSpace
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}