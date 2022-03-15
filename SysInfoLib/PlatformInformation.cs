using SysInfoLib.Services;
using Microsoft.Extensions.Logging;

using static SysInfoLib.Utilities.Misc;

namespace SysInfoLib
{
    public class PlatformInformation
    {
        private readonly IPlatformService _service;
        private readonly ILogger? _logger;

        ///<summary> PlatformInformation constructor </summary>
        ///<param name="logger"> Logger object for logging failed command outputs </param>
        public PlatformInformation(ILogger logger)
        {
            _service = new PlatformService();
            _logger = logger;
        }

        internal PlatformInformation(IPlatformService service)
        {
            _service = service;
        }

        ///<summary> Get name of current distro </summary>
        ///<returns> A string represent the name of distro and version </returns>
        public async Task<string> Distro()
        {
            string osReleaseString = _service.GetOsRelease();
            var distroNameLine = await GrepLineStartsWith(osReleaseString, "PRETTY_NAME=");
            if (string.IsNullOrEmpty(distroNameLine))
            {
                throw new PlatformInfoException("Couldn't propery parse os-release");
            }
            var distroName = distroNameLine.Split('=')[1].Replace("\"","");
            return distroName;
        }

        ///<summary> Get name of linux kernel arch </summary>
        ///<returns> A string represent linux kernel arch </returns>
        public string Arch()
        {
            var cmdOutput = _service.GetArch();

            if (!string.IsNullOrEmpty(cmdOutput.error))
            {
                _logger?.LogError(cmdOutput.error);
                throw new PlatformInfoException($"Getting kernel arch failed.");
            }
            return cmdOutput.output.Trim();
        }

        ///<summary> Get name of linux kernel version </summary>
        ///<returns> A string represent linux kernel version </returns>
        public string KernelVersion()
        {
            var cmdOutput = _service.GetKernelVersion();

            if (!string.IsNullOrEmpty(cmdOutput.error))
            {
                _logger?.LogError(cmdOutput.error);
                throw new PlatformInfoException($"Getting kernel version failed.");
            }
            return cmdOutput.output.Trim();
        }

        ///<summary> Get Hostname </summary>
        ///<returns> Hostname of current machine </returns>
        public string Hostname()
        {
            string hostname = _service.GetHostname();

            if (string.IsNullOrEmpty(hostname))
            {
                throw new PlatformInfoException("Couldn't propery parse hostname");
            }

            return hostname.Trim();;
        }
    }

    ///<summary> Exception to throw a problem occurs during the process getting platform information </summary>
    public class PlatformInfoException : Exception
    {
        public PlatformInfoException(string message) : base(message) {}
    }
};
