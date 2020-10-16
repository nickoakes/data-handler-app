namespace data_handler_app.HubSpotModels
{
    public class ContactDTO
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public ContactDTO(string name, string emailAddress, string phone)
        {
            Name = name;
            EmailAddress = emailAddress;
            Phone = phone;
        }
    }
}