using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace data_handler_app.BugHerdModels
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

        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("local_task_id")]
        public string LocalTaskId { get; set; }

        [JsonProperty("priority_id")]
        public string PriorityId { get; set; }

        [JsonProperty("assigned_to_id")]
        public string AssignedToId { get; set; }

        [JsonProperty("status_id")]
        public string StatusId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("tag_names")]
        public List<string> TagNames { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("requester_id")]
        public string RequesterId { get; set; }

        [JsonProperty("requester_email")]
        public string RequesterEmail { get; set; }

        [JsonProperty("due_at")]
        public DateTimeOffset? DueAt { get; set; }

        [JsonProperty("assignee_ids")]
        public List<string> AssigneeIds { get; set; }

        [JsonProperty("column_id")]
        public string ColumnId { get; set; }
    }
}