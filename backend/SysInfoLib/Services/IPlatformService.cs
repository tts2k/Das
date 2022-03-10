namespace SysInfoLib.Services
{
    internal interface IPlatformService
    {
        ///<summary> PlatformService constructor </summary>
        ///<param name="logger"> Logger object for logging failed command outputs </param>
        string GetOsRelease();

        ///<summary> Get command output containing kernel arch </summary>
        ///<returns> Tuple of command output of kernel arch </returns>>
        (string output, string error) GetArch();

        ///<summary> Get FileStream of OSRELEASE_PATH </summary>
        ///<returns> FileStream of OSRELEASE_PATH </returns>>
        (string output, string error) GetKernelVersion();
    }
}
