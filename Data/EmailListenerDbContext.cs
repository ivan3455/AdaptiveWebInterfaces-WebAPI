using Microsoft.EntityFrameworkCore;

namespace AdaptiveWebInterfaces_WebAPI.Data.Database
{
    public class EmailListenerDbContext : DbContext
    {
        public EmailListenerDbContext(DbContextOptions<EmailListenerDbContext> options) : base(options) { }

        public DbSet<TestTableModel> TestTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestTableModel>().ToTable("TestTable");
        }
    }

    public class TestTableModel
    {
        public int Id { get; set; }
    }
}
