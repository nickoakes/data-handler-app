using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace data_handler_app.EverHourModels
{
    public class ProjectDTO
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
        public BudgetDTO Budget { get; set; }

        [JsonProperty("enableResourcePlanner")]
        public bool EnableResourcePlanner { get; set; }

        public ProjectDTO(ProjectRoot projectRoot)
        {
            CanSyncTasks = projectRoot.CanSyncTasks;
            Users = projectRoot.Users;
            Id = projectRoot.Id;
            Platform = projectRoot.Platform;
            Name = projectRoot.Name;
            CreatedAt = projectRoot.CreatedAt;
            WorkspaceId = projectRoot.WorkspaceId;
            WorkspaceName = projectRoot.WorkspaceName;
            Foreign = projectRoot.Foreign;
            Favorite = projectRoot.Favorite;
            HasWebhook = projectRoot.HasWebhook;
            Status = projectRoot.Status;
            EstimatesType = projectRoot.EstimatesType;
            Budget = new BudgetDTO(projectRoot.Budget);
            EnableResourcePlanner = projectRoot.EnableResourcePlanner;
        }
    }

    public class BudgetDTO
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

        public BudgetDTO(Budget budget)
        {
            ExcludeUnbillableTime = budget.ExcludeUnbillableTime;
            ExcludeExpenses = budget.ExcludeExpenses;
            Period = budget.Period;
            Type = budget.Type;
            
            if(budget.BudgetBudget != null)
            {
                BudgetBudget = budget.BudgetBudget / 3600;
            }
            else
            {
                BudgetBudget = null;
            }

            DisallowOverbudget = budget.DisallowOverbudget;
            ShowToUsers = budget.ShowToUsers;
            Threshold = budget.Threshold;
            
            if(budget.Progress != null)
            {
                Progress = budget.Progress / 3600;
            }
            else
            {
                Progress = null;
            }

            if(budget.TimeProgress != null)
            {
                TimeProgress = budget.TimeProgress / 3600;
            }
            else
            {
                TimeProgress = null;
            }

            ExpenseProgress = budget.ExpenseProgress;
        }
    }
}