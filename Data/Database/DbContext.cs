using AdaptiveWebInterfaces_WebAPI.Models;
using AdaptiveWebInterfaces_WebAPI.Models.User;
using Microsoft.EntityFrameworkCore;

namespace AdaptiveWebInterfaces_WebAPI.Data.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CarModel> Cars { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<GoodModel> Goods { get; set; }
        public DbSet<ManufacturerModel> Manufacturers { get; set; }
        public DbSet<OrderDetailModel> OrderDetails { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
    }
}
