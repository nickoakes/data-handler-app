using Google.Ads.GoogleAds.V4.Services;
using data_handler_app.GoogleAdsMethods;
using System;

namespace data_handler_app.GoogleAdsModels
{
    public class GoogleAdsBudgetDTO
    {
        public decimal? ApprovedSpendingLimit { get; set; }
        public decimal? ProposedSpendingLimit { get; set; }
        public DateTime? ApprovedStartDateTime { get; set; }
        public DateTime? ProposedStartDateTime { get; set; }
        public DateTime? ApprovedEndDateTime { get; set; }
        public DateTime? ProposedEndDateTime { get; set; }
        public decimal? AmountServed { get; set; }
        public GoogleAdsBudgetDTO(GoogleAdsRow resultRoot)
        {
            ApprovedSpendingLimit = BudgetMethods.ConvertMicroToDecimal(resultRoot.AccountBudget.ApprovedSpendingLimitMicros);

            ProposedSpendingLimit = BudgetMethods.ConvertMicroToDecimal(resultRoot.AccountBudget.ProposedSpendingLimitMicros);

            ApprovedStartDateTime = BudgetMethods.ConvertStringToDateTime(resultRoot.AccountBudget.ApprovedStartDateTime);

            ProposedStartDateTime = BudgetMethods.ConvertStringToDateTime(resultRoot.AccountBudget.ProposedStartDateTime);

            ApprovedEndDateTime = BudgetMethods.ConvertStringToDateTime(resultRoot.AccountBudget.ApprovedEndDateTime);

            ProposedEndDateTime = BudgetMethods.ConvertStringToDateTime(resultRoot.AccountBudget.ProposedEndDateTime);

            AmountServed = BudgetMethods.ConvertMicroToDecimal(resultRoot.AccountBudget.AmountServedMicros);
        }
    }
}