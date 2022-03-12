using SysInfoLib.Utilities;

namespace SysInfoLib.Services
{
    internal class PlatformService : IPlatformService
    {
        private readonly CommandExecutor _cmdExecutor;

        //Path to os-release
        private const string OSRELEASE_PATH = "/etc/os-release";

        ///<summary> PlatformService constructor </summary>
        public PlatformService()
        {
            _cmdExecutor = new CommandExecutor();
        }

        ///<summary> Get command output containing kernel arch </summary>
        ///<returns> Tuple of command output of kernel arch </returns>>
        public (string output, string error) GetArch()
        {
            return _cmdExecutor.ExecuteCommand("uname -m");
        }

        ///<summary> Get command output containing kernel version </summary>
        ///<returns> Tuple of command output of kernel version </returns>>
        public (string output, string error) GetKernelVersion()
        {
            return _cmdExecutor.ExecuteCommand("uname -r");
        }

        ///<summary> Get FileStream of OSRELEASE_PATH </summary>
        ///<returns> FileStream of OSRELEASE_PATH </returns>>
        public string GetOsRelease()
        {
            return File.ReadAllText(OSRELEASE_PATH);
        }
    }
}
