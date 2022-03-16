using Microsoft.AspNetCore.Mvc;
using SysInfoLib;

namespace Das.Controllers
{
    [ApiController]
    [Route("api/system")]
    public class SystemInformationController : ControllerBase
    {
        private readonly SystemInformation _sysInfo;

        public SystemInformationController(ILogger<SystemInformationController> logger)
        {
            _sysInfo = new SystemInformation(logger);
        }

        [HttpGet("cpu")]
        [Produces("application/json")]
        public async Task<IActionResult> GetCpuLoad()
        {
            var res = await _sysInfo.CpuLoad(1);
            return Ok(new {value = res});
        }

        [HttpGet("mem")]
        [Produces("application/json")]
        public async Task<IActionResult> GetMemUsage()
        {
            var res = await _sysInfo.MemUsage();
            return Ok(res);
        }

        [HttpGet("uptime")]
        [Produces("application/json")]
        public IActionResult GetUpTime()
        {
            var res = _sysInfo.UpTime();
            return Ok(res);
        }
    }
}
