namespace AdaptiveWebInterfaces_WebAPI.Models
{
    public class GoodModel
    {
        public int GoodId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ManufacturerId { get; set; }
        public int CategoryId { get; set; }
    }
}
