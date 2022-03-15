using System.IO;
using SysInfoLib.Services;

namespace SysInfoLib.Tests
{
    public class PlatformServiceMockGood : IPlatformService
    {
        public (string output, string error) GetKernelVersion()
        {
            var res =  File.ReadAllText("MockData/uname-r.txt");
            return (output: res, error: "");
        }

        public (string output, string error) GetArch()
        {
            var res =  File.ReadAllText("MockData/uname-m.txt");
            return (output: res, error: "");
        }

        public string GetOsRelease()
        {
            var res = File.ReadAllText("MockData/os-release.txt");
            return res;
        }

        public string GetHostname()
        {
            var res = File.ReadAllText("MockData/hostname.txt");
            return res;
        }
    }

    public class PlatformServiceMockBad : IPlatformService
    {
        public (string output, string error) GetKernelVersion() { return (output: "", error: "err"); }
        public (string output, string error) GetArch() { return (output: "", error: "err"); }
        public string GetOsRelease() { return ""; }
        public string GetHostname() { return ""; }
    }
}
