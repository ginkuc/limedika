using System;
using System.Threading.Tasks;
using Limedika.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Limedika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs()
        {
            try
            {
                var logs = await _logService.GetLogs();

                return Ok(logs);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
