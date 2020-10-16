using Newtonsoft.Json;
using System.Collections.Generic;

namespace data_handler_app.HubSpotModels
{
    public partial class EngagementsRoot
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }
        
        [JsonProperty("hasMore")]
        public bool HasMore { get; set; }

        [JsonProperty("offset")]
        public string Offset { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("engagement")]
        public Engagement Engagement { get; set; }

        [JsonProperty("associations")]
        public Associations Associations { get; set; }

        [JsonProperty("attachments")]
        public List<object> Attachments { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }

    public partial class Associations
    {
        [JsonProperty("contactIds")]
        public List<string> ContactIds { get; set; }

        [JsonProperty("companyIds")]
        public List<string> CompanyIds { get; set; }

        [JsonProperty("dealIds")]
        public List<string> DealIds { get; set; }

        [JsonProperty("ownerIds")]
        public List<string> OwnerIds { get; set; }

        [JsonProperty("workflowIds")]
        public List<string> WorkflowIds { get; set; }

        [JsonProperty("ticketIds")]
        public List<string> TicketIds { get; set; }

        [JsonProperty("contentIds")]
        public List<string> ContentIds { get; set; }

        [JsonProperty("quoteIds")]
        public List<string> QuoteIds { get; set; }
    }

    public partial class Engagement
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("portalId")]
        public string PortalId { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }

        [JsonProperty("lastUpdated")]
        public long LastUpdated { get; set; }

        [JsonProperty("createdBy")]
        public long CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long ModifiedBy { get; set; }

        [JsonProperty("ownerId")]
        public string OwnerId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("teamId")]
        public string TeamId { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("allAccessibleTeamIds")]
        public List<string> AllAccessibleTeamIds { get; set; }

        [JsonProperty("bodyPreview")]
        public string BodyPreview { get; set; }

        [JsonProperty("queueMembershipIds")]
        public List<string> QueueMembershipIds { get; set; }

        [JsonProperty("bodyPreviewIsTruncated")]
        public bool BodyPreviewIsTruncated { get; set; }

        [JsonProperty("bodyPreviewHtml")]
        public string BodyPreviewHtml { get; set; }

        [JsonProperty("gdprDeleted")]
        public bool GdprDeleted { get; set; }
    }

    public partial class Metadata
    {
        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore)]
        public From From { get; set; }

        [JsonProperty("to", NullValueHandling = NullValueHandling.Ignore)]
        public List<From> To { get; set; }

        [JsonProperty("cc", NullValueHandling = NullValueHandling.Ignore)]
        public List<From> Cc { get; set; }

        [JsonProperty("bcc", NullValueHandling = NullValueHandling.Ignore)]
        public List<From> Bcc { get; set; }

        [JsonProperty("sender", NullValueHandling = NullValueHandling.Ignore)]
        public EmailSendEventId Sender { get; set; }

        [JsonProperty("subject", NullValueHandling = NullValueHandling.Ignore)]
        public string Subject { get; set; }

        [JsonProperty("html", NullValueHandling = NullValueHandling.Ignore)]
        public string Html { get; set; }

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("emailSendEventId", NullValueHandling = NullValueHandling.Ignore)]
        public EmailSendEventId EmailSendEventId { get; set; }

        [JsonProperty("validationSkipped", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> ValidationSkipped { get; set; }

        [JsonProperty("attachedVideoOpened", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AttachedVideoOpened { get; set; }

        [JsonProperty("attachedVideoWatched", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AttachedVideoWatched { get; set; }

        [JsonProperty("toNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string ToNumber { get; set; }

        [JsonProperty("fromNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string FromNumber { get; set; }

        [JsonProperty("durationMilliseconds", NullValueHandling = NullValueHandling.Ignore)]
        public long? DurationMilliseconds { get; set; }

        [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
        public string Body { get; set; }

        [JsonProperty("disposition", NullValueHandling = NullValueHandling.Ignore)]
        public string Disposition { get; set; }
    }

    public partial class From
    {
        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty("lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }
    }

    public partial class To
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public partial class EmailSendEventId
    {
    }
}
