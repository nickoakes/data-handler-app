using System;

namespace data_handler_app.ShopifyModels
{
    public class SalesDataDTO
    {
        public Guid ID { get; set; }
        public int StoreID { get; set; }
        public DateTime Date { get; set; }
        public decimal Sales { get; set; }
        public SalesDataDTO(DateTime date, int storeID, decimal total)
        {
            ID = Guid.NewGuid();
            StoreID = storeID;
            Date = date;
            Sales = total;
        }
    }
}