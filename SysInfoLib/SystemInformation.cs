using SysInfoLib.Services;
using Microsoft.Extensions.Logging;

using static SysInfoLib.Utilities.Misc;

namespace SysInfoLib
{

    public class SystemInformation
    {
        private readonly ISystemService _service;
        private readonly ILogger? _logger;

        ///<summary> PlatformInformation constructor </summary>
        ///<param name="logger"> Logger object for logging failed command outputs </param>
        public SystemInformation(ILogger logger)
        {
            _service = new SystemService();
            _logger = logger;
        }

        internal SystemInformation(ISystemService service)
        {
            _service = service;
        }

        ///<summary> Get system overall current cpu load </summary>
        ///<param name="interval"> Interval of getting cpu time. Must be positive </param>
        ///<returns> Float represent current cpu load percentage </returns>
        public async Task<decimal> CpuLoad(float interval)
        {
            if (interval < 0) {
                throw new NegativeIntervalException("Interval cannot be a negative number.");
            }

            string cpuInfoString = _service.GetCpuStat();
            string cpuStringLast = await GrepLineStartsWith(cpuInfoString, "cpu  ");
            await Task.Delay((int)interval * 1000);
            cpuInfoString = _service.GetCpuStat();
            string cpuStringCurrent = await GrepLineStartsWith(cpuInfoString, "cpu  ");

            if (String.IsNullOrEmpty(cpuStringLast) || String.IsNullOrEmpty(cpuStringCurrent))
            {
                throw new SysInfoParseException($"Failed to correctly read output from cpuinfo.");
            }

            var lastCpuStat = ParseAndCalcCpuStat(cpuStringLast);
            var currentCpuStat = ParseAndCalcCpuStat(cpuStringCurrent);

            var idleDelta = (decimal)(currentCpuStat.idle - lastCpuStat.idle);
            var totalDelta = (decimal)(currentCpuStat.total - lastCpuStat.total);

            var cpuPercentage = ((totalDelta - idleDelta) / totalDelta);
            return Math.Round(cpuPercentage, 2);
        }

        ///<summary> Get system memory usage </summary>
        ///<returns> Tuple containing total, used and free memory in KB </returns>
        public async Task<MemUsage> MemUsage()
        {
            var memInfoDict = new Dictionary<string, string>();
            var memInfoString = _service.GetMemInfo();

            using (var sr = new StringReader(memInfoString))
            {
                string? line;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    var lineSplitted = line.Split(':');
                    memInfoDict[lineSplitted[0]] = lineSplitted[1].Trim().Split(' ')[0];
                }
            }

            if (memInfoDict.Count() == 0)
            {
                throw new SysInfoParseException($"Failed to correctly read output from meminfo.");
            }

            var memTotal = long.Parse(memInfoDict["MemTotal"]);
            var memFree = long.Parse(memInfoDict["MemFree"]);
            var buffers = long.Parse(memInfoDict["Buffers"]);
            var cached = long.Parse(memInfoDict["Cached"]);
            var sReclaimable = long.Parse(memInfoDict["SReclaimable"]);
            var shmem = long.Parse(memInfoDict["Shmem"]);

            var memUsed = (memTotal - memFree) - (buffers + cached + sReclaimable - shmem);

            return new MemUsage(memTotal, memUsed);
        }

        ///<summary> Get system uptime </summary>
        ///<returns> Tuple containing days, hours and minutes of uptime </returns>
        public Uptime UpTime()
        {
            var uptimeString = _service.GetUpTime();

            if (string.IsNullOrEmpty(uptimeString))
            {
                throw new SysInfoParseException($"Uptime output is incorrect.");
            }

            var uptimeStringArr = uptimeString.Split(' ');
            var uptime = double.Parse(uptimeStringArr[0]);
            var ts = new TimeSpan(0, 0, (int)uptime);

            return new Uptime(ts.Days, ts.Hours, ts.Minutes);
        }

        ///<summary> Parse and then calculate the total stat number of cpu from string </summary>
        ///<param name="cpuString"> String to parse </param>
        ///<returns> Tuple containing total cpu time and idle time </returns>
        private static (long total, long idle) ParseAndCalcCpuStat(string cpuString)
        {
            var cpuStringSplited = cpuString.Split(' ').Skip(2);
            var cpuStat = cpuStringSplited.Select(long.Parse).ToArray();

            // CPU stat table:
            // user nice system idle iowait irq softirq steal guest guest_nice
            var idle = cpuStat[3] + cpuStat[4];
            var nonIdle = cpuStat[0] + cpuStat[1] + cpuStat[2] + cpuStat[5] + cpuStat[6] + cpuStat[7];

            return (total: idle + nonIdle, idle: idle);
        }
    }

    ///<summary> Exception to throw when interval number is negative </summary>
    public class NegativeIntervalException : Exception
    {
        public NegativeIntervalException(string message) : base(message) {}
    }

    ///<summary> Exception to throw when parsing something from /proc/ failed </summary>
    public class SysInfoParseException : Exception
    {
        public SysInfoParseException(string message) : base(message) {}
    }
}
