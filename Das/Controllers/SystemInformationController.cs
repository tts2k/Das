using Microsoft.AspNetCore.Mvc;
using SysInfoLib;

namespace Das.Controllers
{
    [ApiController]
    [Route("system")]
    public class SystemInformationController : ControllerBase
    {
        private readonly SystemInformation _sysinfo;

        public SystemInformationController(ILogger<SystemInformationController> logger)
        {
            _sysinfo = new SystemInformation(logger);
        }

        [HttpGet("cpu")]
        [Produces("application/json")]
        public async Task<IActionResult> GetCpuLoad()
        {
            var res = await _sysinfo.CpuLoad(1);
            return Ok(new {value = res});
        }

        [HttpGet("mem")]
        [Produces("application/json")]
        public async Task<IActionResult> GetMemUsage()
        {
            var res = await _sysinfo.MemUsage();
            return Ok(res);
        }

        [HttpGet("uptime")]
        [Produces("application/json")]
        public IActionResult GetUpTime()
        {
            var res = _sysinfo.UpTime();
            return Ok(res);
        }
    }
}
