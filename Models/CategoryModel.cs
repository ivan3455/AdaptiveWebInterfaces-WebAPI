using System.ComponentModel.DataAnnotations;

namespace AdaptiveWebInterfaces_WebAPI.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
