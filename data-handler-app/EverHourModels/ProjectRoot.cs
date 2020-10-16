using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace data_handler_app.EverHourModels
{
    public partial class ProjectRoot
    {
        [JsonProperty("canSyncTasks")]
        public bool CanSyncTasks { get; set; }

        [JsonProperty("users")]
        public List<string> Users { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("workspaceId")]
        public string WorkspaceId { get; set; }

        [JsonProperty("workspaceName")]
        public string WorkspaceName { get; set; }

        [JsonProperty("foreign")]
        public bool Foreign { get; set; }

        [JsonProperty("favorite")]
        public bool Favorite { get; set; }

        [JsonProperty("hasWebhook")]
        public bool HasWebhook { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("estimatesType")]
        public string EstimatesType { get; set; }

        [JsonProperty("budget")]
        public Budget Budget { get; set; }

        [JsonProperty("enableResourcePlanner")]
        public bool EnableResourcePlanner { get; set; }
    }

    public partial class Budget
    {
        [JsonProperty("excludeUnbillableTime")]
        public bool ExcludeUnbillableTime { get; set; }

        [JsonProperty("excludeExpenses")]
        public bool ExcludeExpenses { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("budget")]
        public int? BudgetBudget { get; set; }

        [JsonProperty("disallowOverbudget")]
        public bool DisallowOverbudget { get; set; }

        [JsonProperty("showToUsers")]
        public bool ShowToUsers { get; set; }

        [JsonProperty("threshold")]
        public int? Threshold { get; set; }

        [JsonProperty("progress")]
        public int? Progress { get; set; }

        [JsonProperty("timeProgress")]
        public int? TimeProgress { get; set; }

        [JsonProperty("expenseProgress")]
        public int? ExpenseProgress { get; set; }
    }
}