namespace AdaptiveWebInterfaces_WebAPI.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public decimal TotalSum { get; set; }
    }
}
