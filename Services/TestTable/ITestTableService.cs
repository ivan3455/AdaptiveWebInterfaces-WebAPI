using AdaptiveWebInterfaces_WebAPI.Data.Database;
using System.Threading.Tasks;

namespace AdaptiveWebInterfaces_WebAPI.Services
{
    public interface ITestTableService
    {
        Task<TestTableModel> AddTestTableEntryAsync(int id);
    }
}
