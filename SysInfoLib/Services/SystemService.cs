using SysInfoLib.Utilities;

namespace SysInfoLib.Services
{
    internal class SystemService : ISystemService
    {
        // Linux system info file location
        private const string MEMINFO_PATH = "/proc/meminfo";
        private const string CPUSTAT_PATH = "/proc/stat";
        private const string UPTIME_PATH = "/proc/uptime";

        ///<summary> Get FileStream of CPUSTATH_PATH </summary>
        ///<returns> FileStream of CPUSTATH_PATH</returns>>
        public string GetCpuStat()
        {
            return File.ReadAllText(CPUSTAT_PATH);
        }

        ///<summary> Get FileStream of MEMINFO_PATH </summary>
        ///<returns> FileStream of MEMINFO_PATH </returns>>
        public string GetMemInfo()
        {
            return File.ReadAllText(MEMINFO_PATH);
        }

        ///<summary> Get FileStream of MEMINFO_PATH </summary>
        ///<returns> FileStream of MEMINFO_PATH </returns>>
        public string GetUpTime()
        {
            return File.ReadAllText(UPTIME_PATH);
        }
    }
}
