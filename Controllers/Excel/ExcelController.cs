using AdaptiveWebInterfaces_WebAPI.Services.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdaptiveWebInterfaces_WebAPI.Controllers.Excel
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Authorize]
    public class ExcelController : ControllerBase
    {
        private readonly IExcelService _excelService;

        public ExcelController(IExcelService excelService)
        {
            _excelService = excelService;
        }

        [HttpGet]
        public IActionResult Get(ApiVersion version)
        {
            if (version.ToString() == "1.0")
            {
                Random rnd = new Random();
                int randomNumber = rnd.Next(1, 100);
                return Ok(randomNumber);
            }
            else if (version.ToString() == "2.0")
            {
                return Ok("Hello! -- Version 2.0");
            }
            else if (version.ToString() == "3.0")
            {
                var stream = new MemoryStream();
                _excelService.GenerateExcel();
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "example.xlsx");
            }
            else
            {
                return BadRequest("Invalid API version");
            }
        }
    }
}

