using System;

namespace data_handler_app.GoogleAdsMethods
{
    public static class BudgetMethods
    {
        public static DateTime? ConvertStringToDateTime(string dateTime)
        {
            DateTime approvedStartDateTime;

            if (DateTime.TryParse(dateTime, out approvedStartDateTime))
            {
                return approvedStartDateTime;
            }
            else
            {
                return null;
            }
        }

        public static decimal? ConvertMicroToDecimal(long? micro)
        {
            if(micro != null)
            {
                return micro / 1000000;
            }
            else
            {
                return null;
            }
        }

        public static double? ConvertMicroToDouble(long? micro)
        {
            if(micro != null)
            {
                return micro / 1000000;
            }
            else
            {
                return null;
            }
        }

        public static double? ConvertMicroToDouble(double? micro)
        {
            if (micro != null)
            {
                return micro / 1000000;
            }
            else
            {
                return null;
            }
        }
    }
}