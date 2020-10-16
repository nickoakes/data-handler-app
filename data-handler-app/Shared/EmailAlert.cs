using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace data_handler_app.Shared
{
    public static class EmailAlert
    {
        public async static Task<string> SendEmailAlert(EmailAddress recipient, string subject, string body)
        {
            Dictionary<string, string> emailConfig = ConfigDictionary.EmailConfig();

            var message = new SendGridMessage();

            // Set the sender name and email address for alerts (CUSTOM_FIELD)

            message.SetFrom(new EmailAddress("ALERT_SENDER_EMAIL", "ALERT_SENDER_NAME"));

            message.AddTo(recipient);

            message.SetSubject(subject);

            message.AddContent(MimeType.Html, body);

            var client = new SendGridClient(emailConfig["SMTPPassword"]);

            try
            {
                await client.SendEmailAsync(message);

                return "Email sent!";
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());

                return "Email failed to send.";
            }
        }
    }
}