namespace SysInfoLib.Services
{
    internal interface IPlatformService
    {
        ///<summary> Read content of OSRELEASE_PATH </summary>
        ///<returns> String with content of OSRELEASE_PATH </returns>
        string GetOsRelease();

        ///<summary> Get command output containing kernel arch </summary>
        ///<returns> Tuple of command output of kernel arch </returns>
        (string output, string error) GetArch();

        ///<summary> Get command output containing kernel version </summary>
        ///<returns> Tuple of command output of kernel version </returns>
        (string output, string error) GetKernelVersion();

        ///<summary> Read content of HOSTNAME_PATH </summary>
        ///<returns> String with contain of HOSTNAME_PATH </returns>
        string GetHostname();
    }
}
