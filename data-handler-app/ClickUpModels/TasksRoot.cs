using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace data_handler_app.ClickUpModels
{
    public partial class TasksRoot
    {
        [JsonProperty("tasks")]
        public List<Task> Tasks { get; set; }
    }

    public partial class Task
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public TasksStatus Status { get; set; }

        [JsonProperty("orderindex")]
        public decimal Orderindex { get; set; }

        [JsonProperty("date_created")]
        public string DateCreated { get; set; }

        [JsonProperty("date_updated")]
        public string DateUpdated { get; set; }

        [JsonProperty("date_closed")]
        public string DateClosed { get; set; }

        [JsonProperty("creator")]
        public User Creator { get; set; }

        [JsonProperty("assignees")]
        public List<User> Assignees { get; set; }

        [JsonProperty("checklists")]
        public List<object> Checklists { get; set; }

        [JsonProperty("tags")]
        public List<object> Tags { get; set; }

        [JsonProperty("parent")]
        public object Parent { get; set; }

        [JsonProperty("priority")]
        public object Priority { get; set; }

        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("time_estimate")]
        public int? TimeEstimate { get; set; }

        [JsonProperty("time_spent")]
        public int? TimeSpent { get; set; }

        [JsonProperty("list")]
        public Folder List { get; set; }

        [JsonProperty("folder")]
        public Folder Folder { get; set; }

        [JsonProperty("space")]
        public Folder Space { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class TasksFolder
    {
        [JsonProperty("id")]
        public string FolderId { get; set; }
    }

    public partial class TasksStatus
    {
        [JsonProperty("status")]
        public string StatusStatus { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("orderindex")]
        public int? Orderindex { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}