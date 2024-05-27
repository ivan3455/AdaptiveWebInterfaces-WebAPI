using System.ComponentModel.DataAnnotations;

namespace AdaptiveWebInterfaces_WebAPI.Models
{
    public class ManufacturerModel
    {
        [Key]
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Contacts { get; set; }
    }
}
