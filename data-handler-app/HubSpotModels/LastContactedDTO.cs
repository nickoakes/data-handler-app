namespace data_handler_app.HubSpotModels
{
    public class LastContactedDTO
    {
        public string CompanyName { get; set; }
        public double DaysSinceLastContact { get; set; }
        public LastContactedDTO(string companyName, double daysSinceLastContact)
        {
            CompanyName = companyName;
            DaysSinceLastContact = daysSinceLastContact;
        }
    }
}