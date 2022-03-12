using System.IO;
using SysInfoLib.Services;

namespace SysInfoLib.Tests
{
    public class SystemServiceMockGood : ISystemService
    {
        private ushort currentStatFile = 0;

        public string GetCpuStat() {
            if (currentStatFile == 0)
            {
                currentStatFile = 1;
                return File.ReadAllText("MockData/cpustat.txt");
            }
            else
            {
                currentStatFile = 0;
                return File.ReadAllText("MockData/cpustat1.txt");
            }
        }
        public string GetMemInfo() { return File.ReadAllText("MockData/meminfo.txt"); }
        public string GetUpTime() { return File.ReadAllText("MockData/uptime.txt"); }
    }

    public class SystemServiceMockBad : ISystemService
    {
        public string GetCpuStat() { return ""; }
        public string GetMemInfo() { return ""; }
        public string GetUpTime() { return ""; }
    }
}
