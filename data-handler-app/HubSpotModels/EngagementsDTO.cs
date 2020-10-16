using data_handler_app.HubSpotMethods;
using System;
using System.Collections.Generic;

namespace data_handler_app.HubSpotModels
{
    public class EngagementsDTO
    {
        public string EngagementType { get; set; }
        public string Direction { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        public EngagementsContact Sender { get; set; }
        public List<EngagementsContact> Recipients { get; set; }
        public List<EngagementsContact> CC { get; set; }
        public List<EngagementsContact> BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromNumber { get; set; }
        public string ToNumber { get; set; }
        public long? CallDurationSeconds { get; set; }
        public EngagementsDTO(Result engagement)
        {
            EngagementType = engagement.Engagement.Type;
            CreatedAt = DateTimeOffset.FromUnixTimeMilliseconds(engagement.Engagement.CreatedAt);
            LastUpdatedAt = DateTimeOffset.FromUnixTimeMilliseconds(engagement.Engagement.LastUpdated);

            if(string.Equals(engagement.Engagement.Type, "EMAIL") || string.Equals(engagement.Engagement.Type, "INCOMING_EMAIL"))
            {
                //Identify outbound emails by checking the 'From' address for the company name (CUSTOM_FIELD)

                if (engagement.Metadata.From.Email.Contains("COMPANY_NAME"))
                {
                    Direction = "Outbound";
                }
                else
                {
                    Direction = "Inbound";
                }

                Sender = new EngagementsContact($"{engagement.Metadata.From.FirstName} {engagement.Metadata.From.LastName}", engagement.Metadata.From.Email);
                Recipients = EngagementsSearch.CreateContactList(engagement.Metadata.To);
                CC = EngagementsSearch.CreateContactList(engagement.Metadata.Cc);
                BCC = EngagementsSearch.CreateContactList(engagement.Metadata.Bcc);
                Subject = engagement.Metadata.Subject;
                Body = engagement.Engagement.BodyPreview;
                FromNumber = null;
                ToNumber = null;
                CallDurationSeconds = null;
            }
            else if(string.Equals(engagement.Engagement.Type, "CALL"))
            {
                if(engagement.Engagement.BodyPreview.Contains("Inbound answered"))
                {
                    Direction = "Inbound";
                }
                else
                {
                    Direction = "Outbound";
                }

                Sender = null;
                Recipients = null;
                CC = null;
                BCC = null;
                Subject = null;
                Body = null;
                FromNumber = engagement.Metadata.FromNumber;
                ToNumber = engagement.Metadata.ToNumber;
                CallDurationSeconds = engagement.Metadata.DurationMilliseconds / 1000;
            }
            else if(string.Equals(engagement.Engagement.Type, "TASK"))
            {
                Direction = null;
                Sender = null;
                Recipients = null;
                CC = null;
                BCC = null;
                Subject = engagement.Metadata.Subject;
                Body = null;
                FromNumber = null;
                ToNumber = null;
                CallDurationSeconds = null;
            }
            else
            {
                Direction = null;
                Sender = null;
                Recipients = null;
                CC = null;
                BCC = null;
                Subject = null;
                Body = null;
                FromNumber = null;
                ToNumber = null;
                CallDurationSeconds = null;
            }
        }
    }

    public class EngagementsContact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public EngagementsContact(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}