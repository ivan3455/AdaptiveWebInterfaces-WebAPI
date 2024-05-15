using System.ComponentModel.DataAnnotations;

namespace AdaptiveWebInterfaces_WebAPI.Models
{
    public class OrderDetailModel
    {
        [Key]
        public int OrderId { get; set; }
        public int GoodId { get; set; }
        public int Number { get; set; }
    }
}
