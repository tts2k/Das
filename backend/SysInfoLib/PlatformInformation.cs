using SysInfoLib.Services;

using static SysInfoLib.Utilities.Misc;

namespace SysInfoLib
{
    public class PlatformInformation
    {
        private readonly IPlatformService _service;

        ///<summary> PlatformInformation constructor </summary>
        ///<param name="logger"> Logger object for logging failed command outputs </param>
        public PlatformInformation()
        {
            _service = new PlatformService();
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
                throw new PlatformInfoException($"Getting kernel arch failed: {cmdOutput.error}");
            }
            return cmdOutput.output;
        }

        ///<summary> Get name of linux kernel version </summary>
        ///<returns> A string represent linux kernel version </returns>
        public string KernelVersion()
        {
            var cmdOutput = _service.GetKernelVersion();

            if (!string.IsNullOrEmpty(cmdOutput.error))
            {
                throw new PlatformInfoException($"Getting kernel version failed: {cmdOutput.error}");
            }
            return cmdOutput.output;
        }
    }

    ///<summary> Exception to throw a problem occurs during the process getting platform information </summary>
    public class PlatformInfoException : Exception
    {
        public PlatformInfoException(string message) : base(message) {}
    }
};
