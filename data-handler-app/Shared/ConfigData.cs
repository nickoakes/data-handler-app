using System.Collections.Generic;
using System.Configuration;

namespace data_handler_app.Shared
{
    public static class ConfigDictionary
    {
        public static Dictionary<string, string> Config()
        {
            Dictionary<string, string> config = new Dictionary<string, string>();

            config.Add("ClientID", ConfigurationManager.AppSettings.Get("ClientID"));
            config.Add("ClientSecret", ConfigurationManager.AppSettings.Get("ClientSecret"));
            config.Add("HubSpotAPIKey", ConfigurationManager.AppSettings.Get("HubSpotAPIKey"));
            config.Add("HubSpotAPIURLRoot", ConfigurationManager.AppSettings.Get("HubSpotAPIURLRoot"));
            config.Add("ClickUpAPIKey", ConfigurationManager.AppSettings.Get("ClickUpAPIKey"));
            config.Add("ClickUpProjectsSpaceID", ConfigurationManager.AppSettings.Get("ClickUpProjectsSpaceID"));
            config.Add("ClickUpRetainersSpaceID", ConfigurationManager.AppSettings.Get("ClickUpRetainersSpaceID"));
            config.Add("ClickUpAPIURLRoot", ConfigurationManager.AppSettings.Get("ClickUpAPIURLRoot"));
            config.Add("BugHerdURLRoot", ConfigurationManager.AppSettings.Get("BugHerdURLRoot"));
            config.Add("BugHerdAPIKey", ConfigurationManager.AppSettings.Get("BugHerdAPIKey"));
            config.Add("BugHerdAPIPassword", ConfigurationManager.AppSettings.Get("BugHerdAPIPassword"));
            config.Add("EverHourURLRoot", ConfigurationManager.AppSettings.Get("EverHourURLRoot"));
            config.Add("EverHourAPIKey", ConfigurationManager.AppSettings.Get("EverHourAPIKey"));
            config.Add("ShopifyAdminAPIVersion", ConfigurationManager.AppSettings.Get("ShopifyAdminAPIVersion"));
            config.Add("FacebookAPIURLRoot", ConfigurationManager.AppSettings.Get("FacebookAPIURLRoot"));
            config.Add("FacebookAccessToken", ConfigurationManager.AppSettings.Get("FacebookAccessToken"));
            config.Add("FacebookAPIVersion", ConfigurationManager.AppSettings.Get("FacebookAPIVersion"));

            return config;
        }

        public static Dictionary<string, string> EmailConfig()
        {
            Dictionary<string, string> emailConfig = new Dictionary<string, string>();

            emailConfig.Add("GoogleAdsRecipientAddress", ConfigurationManager.AppSettings.Get("GoogleAdsRecipientAddress"));
            emailConfig.Add("SalesTargetRecipientAddress", ConfigurationManager.AppSettings.Get("SalesTargetRecipientAddress"));
            emailConfig.Add("LastContactedRecipientAddress", ConfigurationManager.AppSettings.Get("LastContactedRecipientAddress"));
            emailConfig.Add("SMTPClient", ConfigurationManager.AppSettings.Get("SMTPClient"));
            emailConfig.Add("SMTPPassword", ConfigurationManager.AppSettings.Get("SMTPPassword"));

            return emailConfig;
        }
    }
}