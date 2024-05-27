using AdaptiveWebInterfaces_WebAPI.Data.Database;
using System.Threading.Tasks;

namespace AdaptiveWebInterfaces_WebAPI.Services
{
    public class TestTableService : ITestTableService
    {
        private readonly EmailListenerDbContext _context;

        public TestTableService(EmailListenerDbContext context)
        {
            _context = context;
        }

        public async Task<TestTableModel> AddTestTableEntryAsync(int id)
        {
            var entry = new TestTableModel { Id = id };
            _context.TestTable.Add(entry);
            await _context.SaveChangesAsync();
            return entry;
        }
    }
}
