using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace data_handler_app.ClickUpModels
{
    public partial class TaskDTO
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
            public DateTimeOffset? DateCreated { get; set; }

            [JsonProperty("date_updated")]
            public DateTimeOffset? DateUpdated { get; set; }

            [JsonProperty("date_closed")]
            public DateTimeOffset? DateClosed { get; set; }

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
            public DateTimeOffset? DueDate { get; set; }

            [JsonProperty("start_date")]
            public DateTimeOffset? StartDate { get; set; }

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

            public TaskDTO(Task task)
            {
                Id = task.Id;
                Name = task.Name;
                Status = task.Status;
                Orderindex = task.Orderindex;

                if (!string.IsNullOrEmpty(task.DateCreated))
                {
                    DateCreated = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(task.DateCreated));
                }
                else
                {
                    DateCreated = null;
                }

                if (!string.IsNullOrEmpty(task.DateUpdated))
                {
                    DateUpdated = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(task.DateUpdated));
                }
                else
                {
                    DateUpdated = null;
                }

                if (!string.IsNullOrEmpty(task.DateClosed))
                {
                    DateClosed = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(task.DateClosed));
                }
                else
                {
                    DateClosed = null;
                }

                Creator = task.Creator;
                Assignees = task.Assignees;
                Checklists = task.Checklists;
                Tags = task.Tags;
                Parent = task.Parent;
                Priority = task.Priority;

                if (!string.IsNullOrEmpty(task.DueDate))
                {
                    DueDate = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(task.DueDate));
                }
                else
                {
                    DueDate = null;
                }

                if (!string.IsNullOrEmpty(task.StartDate))
                {
                    StartDate = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(task.StartDate));
                }
                else
                {
                    StartDate = null;
                }

                if (task.TimeEstimate != null)
                {
                    TimeEstimate = task.TimeEstimate / 3600000;
                }
                else
                {
                    TimeEstimate = null;
                }

                if (task.TimeSpent != null)
                {
                    TimeSpent = task.TimeSpent / 3600000;
                }
                else
                {
                    TimeSpent = null;
                }

                List = task.List;
                Folder = task.Folder;
                Space = task.Space;
                Url = task.Url;
            }
        }

        public partial class TasksFolderDTO
        {
            [JsonProperty("id")]
            public string FolderId { get; set; }
        }

        public partial class TasksStatusDTO
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