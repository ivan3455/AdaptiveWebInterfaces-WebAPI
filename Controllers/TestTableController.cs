using AdaptiveWebInterfaces_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AdaptiveWebInterfaces_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestTableController : ControllerBase
    {
        private readonly ITestTableService _testTableService;
        private readonly IEmailService _emailService;

        public TestTableController(ITestTableService testTableService, IEmailService emailService)
        {
            _testTableService = testTableService;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTestTableEntry(int id)
        {
            using (var activity = Telemetry.ActivitySource.StartActivity("AddTestTableEntry"))
            {
                activity?.SetTag("parameter.id", id);
                activity?.SetTag("user.id", User?.Identity?.Name ?? "anonymous");

                await _testTableService.AddTestTableEntryAsync(id);
                await _emailService.SendEmailNotificationAsync(id);

                activity?.SetTag("result.status", "success");
            }
            return Ok();
        }
    }
}
