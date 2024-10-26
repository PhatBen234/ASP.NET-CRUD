namespace SalesOrderDALApp.Models
{
    public class SalesOrder
    {
        public string SalesOrderId { get; set; }
        public string SalesOrderItem { get; set; }
        public string WorkOrder { get; set; }
        public string ProductID { get; set; }
        public string ProductDescription { get; set; }
        public decimal OrderQuantity { get; set; }
        public string OrderStatus { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
