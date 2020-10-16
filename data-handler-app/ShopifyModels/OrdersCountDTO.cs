namespace data_handler_app.ShopifyModels
{
    public struct OrdersCountDTO
    {
        public int? OrdersThisMonth { get; set; }
        public int? OrdersLastMonth { get; set; }
        public int? OrdersMonthBeforeLast { get; set; }
        public OrdersCountDTO(int? ordersThisMonth, int? ordersLastMonth, int? ordersMonthBeforeLast)
        {
            OrdersThisMonth = ordersThisMonth;
            OrdersLastMonth = ordersLastMonth;
            OrdersMonthBeforeLast = ordersMonthBeforeLast;
        }
    }
}