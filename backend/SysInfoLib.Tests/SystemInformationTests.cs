using Xunit;
using System;

namespace SysInfoLib.Tests
{
    public class SystemInformationTests
    {
        private readonly SystemInformation _good;
        private readonly SystemInformation _bad;


        public SystemInformationTests()
        {
            _good = new SystemInformation(new SystemServiceMockGood());
            _bad = new SystemInformation(new SystemServiceMockBad());
        }

        [Fact]
        public async void CpuLoad_Good()
        {
            var res = await _good.CpuLoad(0);
            Assert.Equal((float)Math.Round((decimal)0.0019141372709871479, 2), res);
        }

        [Fact]
        public async void MemUsage_Good()
        {
            var res = await _good.MemUsage();
            Assert.Equal(14269144, res.total);
            Assert.Equal(4831792, res.used);
            Assert.Equal(9437352, res.free);
        }

        [Fact]
        public void UpTime_Good()
        {
            var res = _good.UpTime();
            Assert.Equal(5, res.day);
            Assert.Equal(17, res.hour);
            Assert.Equal(26, res.min);
        }

        [Fact]
        public async void CpuLoad_Empty_Bad()
        {
            await Assert.ThrowsAsync<SysInfoParseException>(() => _bad.CpuLoad(0));
        }

        [Fact]
        public async void CpuLoad_NegativeInterval_Bad()
        {
            await Assert.ThrowsAsync<NegativeIntervalException>(() => _bad.CpuLoad(-1));
        }

        [Fact]
        public async void MemUsage_Empty_Bad()
        {
            await Assert.ThrowsAsync<SysInfoParseException>(() => _bad.MemUsage());
        }

        [Fact]
        public void Uptime_Empty_Bad()
        {
            Assert.Throws<SysInfoParseException>(() => _bad.UpTime());
        }
    }
}
