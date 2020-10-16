using data_handler_app.HubSpotModels;
using data_handler_app.Shared;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace data_handler_app.HubSpotMethods
{
    public class LastContacted
    {
        public static async Task<dynamic> CheckDateLastContacted(HttpClient client, Entities db, List<string> companyNames)
        {
            var searchProps = new string[] { "notes_last_contacted" };

            var alertCompanies = new List<LastContactedDTO>();

                foreach(string company in companyNames)
                {
                    try
                    {
                        CompanyRoot.Company companyData = await HubSpotShared.GetHubSpotInfo(client, db, company, searchProps);

                        DateTimeOffset? lastContact = companyData
                                                      .Results
                                                      .FirstOrDefault()
                                                      .Properties
                                                      .NotesLastContacted;

                        if(lastContact != null)
                        {
                            double daysSinceLastContact = (DateTime.Now - lastContact.GetValueOrDefault().Date).TotalDays;

                            if (daysSinceLastContact > 7)
                            {
                                alertCompanies.Add(new LastContactedDTO(company, daysSinceLastContact));
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"{ company } - { ex }");
                    }
                }

            return await SendEmailAlert(alertCompanies);
        }

        public async static Task<string> SendEmailAlert(List<LastContactedDTO> companies)
        {
            var emailConfig = ConfigDictionary.EmailConfig();

            string alertCompanies = string.Empty;

            foreach (LastContactedDTO company in companies)
            {
                alertCompanies += $"<li><b>{ company.CompanyName }</b>: last contacted over <span style='color:red;font-weight:bold;'>{ Math.Floor(company.DaysSinceLastContact) }</span> days ago</li>";
            }

            string messageBody = "<html><body>" +
                                $"<span style='display:inline-block;'><img src='https://cdn.shopify.com/s/files/applications/75204543d0fec5bbf9e7f1db609e8804_512x512.png?1594118407' style='width:50%;'/><h2>Client Communication Alert</h2></span>" +
                                $"<p>It has been over 7 days since the following clients were last contacted:</p><br/>" +
                                $"<ul>{ alertCompanies }</ul>" +
                                "</body></html>";

            try
            {
                await EmailAlert.SendEmailAlert(new EmailAddress(emailConfig["LastContactedRecipientAddress"]),
                                                $"Client Communication Alert.",
                                                messageBody);

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