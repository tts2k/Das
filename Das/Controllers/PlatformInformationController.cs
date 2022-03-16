using Microsoft.AspNetCore.Mvc;
using SysInfoLib;

namespace Das.Controllers
{
    [ApiController]
    [Route("api/platform")]
    public class PlatformInformationController : ControllerBase
    {
        private readonly PlatformInformation _platformInfo;

        public PlatformInformationController(ILogger<SystemInformationController> logger)
        {
            _platformInfo = new PlatformInformation(logger);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetDistro()
        {
            var distro = await _platformInfo.Distro();
            var arch = _platformInfo.Arch();
            var kernel = _platformInfo.KernelVersion();
            return Ok(new Platform(distro, arch, kernel));
        }

        [HttpGet("hostname")]
        [Produces("application/json")]
        public IActionResult GetPlatform()
        {
            var res = _platformInfo.Hostname();
            return Ok(new {value = res});
        }
    }
}
