using Newtonsoft.Json;
using System.Collections.Generic;

namespace data_handler_app.ClickUpModels
{
    public partial class ListsRoot
    {
        [JsonProperty("lists")]
        public List<List> Lists { get; set; }
    }

    public partial class List
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("orderindex")]
        public int? Orderindex { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("status")]
        public ListPurpleStatus Status { get; set; }

        [JsonProperty("priority")]
        public Priority Priority { get; set; }

        [JsonProperty("assignee")]
        public User Assignee { get; set; }

        [JsonProperty("task_count")]
        public int? TaskCount { get; set; }

        [JsonProperty("due_date")]
        public int? DueDate { get; set; }

        [JsonProperty("due_date_time")]
        public bool? DueDateTime { get; set; }

        [JsonProperty("start_date")]
        public int? StartDate { get; set; }

        [JsonProperty("start_date_time")]
        public int? StartDateTime { get; set; }

        [JsonProperty("folder")]
        public Folder Folder { get; set; }

        [JsonProperty("space")]
        public Folder Space { get; set; }

        [JsonProperty("statuses")]
        public List<ListStatusElement> Statuses { get; set; }

        [JsonProperty("inbound_address")]
        public string InboundAddress { get; set; }
    }

    public partial class ListFolder
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("hidden")]
        public bool? Hidden { get; set; }

        [JsonProperty("access")]
        public bool? Access { get; set; }
    }

    public partial class Priority
    {
        [JsonProperty("priority")]
        public string PriorityPriority { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }

    public partial class ListPurpleStatus
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("hide_label")]
        public bool? HideLabel { get; set; }
    }

    public partial class ListStatusElement
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
}