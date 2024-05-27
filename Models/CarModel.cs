using System.ComponentModel.DataAnnotations;

namespace AdaptiveWebInterfaces_WebAPI.Models
{
    public class CarModel
    {
        [Key]
        public int CarId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
