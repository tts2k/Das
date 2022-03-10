namespace SysInfoLib.Services
{
    internal interface ISystemService
    {
        ///<summary> Get FileStream of /proc/cpuinfo </summary>
        ///<returns> FileStream of /proc/cpuinfo</returns>>
        string GetCpuStat();
        
        ///<summary> Get FileStream of /proc/meminfo </summary>
        ///<returns> FileStream of /proc/meminfo </returns>>
        string GetMemInfo();

        ///<summary> Get command output of uptime </summary>
        ///<returns> Tuple of command output of uptime </returns>>
        (string output, string error) GetUpTime();
    }
}
