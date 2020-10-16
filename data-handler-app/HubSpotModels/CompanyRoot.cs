using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace data_handler_app.HubSpotModels
{
    public class CompanyRoot
    {
            public partial class Company
            {
                [JsonProperty("total")]
                public int? Total { get; set; }

                [JsonProperty("results")]
                public List<Result> Results { get; set; }
            }

            public partial class Result
            {
                [JsonProperty("id")]
                public string Id { get; set; }

                [JsonProperty("properties")]
                public Properties Properties { get; set; }

                [JsonProperty("createdAt")]
                public DateTimeOffset? CreatedAt { get; set; }

                [JsonProperty("updatedAt")]
                public DateTimeOffset? UpdatedAt { get; set; }

                [JsonProperty("archived")]
                public bool Archived { get; set; }
            }

            public partial class Properties
            {
                [JsonProperty("createdate")]
                public DateTimeOffset? Createdate { get; set; }

                [JsonProperty("contract_start_date")]
                public DateTimeOffset? ContractStartDate { get; set; }

                [JsonProperty("domain")]
                public string Domain { get; set; }

                [JsonProperty("hs_lastmodifieddate")]
                public DateTimeOffset? HsLastmodifieddate { get; set; }

                [JsonProperty("hs_object_id")]
                public string HsObjectId { get; set; }

                [JsonProperty("monthly_ad_budget")]
                public decimal? MonthlyAdBudget { get; set; }

                [JsonProperty("monthly_retainer")]
                public decimal? MonthlyRetainer { get; set; }

                [JsonProperty("notes_last_contacted")]
                public DateTimeOffset? NotesLastContacted { get; set; }

                [JsonProperty("num_contacted_notes")]
                public int? NumContactedNotes { get; set; }
        }
    }
}