using System;

namespace data_handler_app.HubSpotModels
{
    public class CompanyDTO
    {
        public CompanyInfo CompanyInfo { get; set; }

        public CompanyDTO(string url,
                          DateTimeOffset? contractStartDate,
                          decimal? monthlyRetainer,
                          decimal? adBudget,
                          DateTimeOffset? lastContact,
                          int? timesContacted, 
                          DateTimeOffset? lastModified)
        {
            CompanyInfo = new CompanyInfo(url, contractStartDate, monthlyRetainer, adBudget, lastContact, timesContacted, lastModified);
        }
    }

    public class CompanyInfo
    {
        public string URL { get; set; }
        public DateTimeOffset? ContractStartDate { get; set; }
        public decimal? MonthlyRetainer { get; set; }
        public decimal? AdBudget { get; set; }
        public DateTimeOffset? LastContact { get; set; }
        public int? TimesContacted { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public CompanyInfo(string url, DateTimeOffset? contractStartDate, decimal? monthlyRetainer, decimal? adBudget, DateTimeOffset? lastContact, int? timesContacted, DateTimeOffset? lastModified)
        {
            URL = url;
            ContractStartDate = contractStartDate;
            MonthlyRetainer = monthlyRetainer;
            AdBudget = adBudget;
            LastContact = lastContact;
            TimesContacted = timesContacted;
            LastModified = lastModified;
        }
    }
}