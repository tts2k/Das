using Xunit;

namespace SysInfoLib.Tests
{
   public class PlatformInformationTests
   {
        private readonly PlatformInformation _good;
        private readonly PlatformInformation _bad;

        public PlatformInformationTests()
        {
             _good = new PlatformInformation(new PlatformServiceMockGood());
             _bad = new PlatformInformation(new PlatformServiceMockBad());
        }

        [Fact]
        public async void Distro_Good()
        {
            var res = await _good.Distro();
            Assert.Equal("NixOS 21.11 (Porcupine)", res);
        }

        [Fact]
        public void Arch_Good()
        {
            var res = _good.Arch();
            Assert.Equal("x86_64\n", res);
        }

        [Fact]
        public void KernelVersion_Good()
        {
            var res = _good.KernelVersion();
            Assert.Equal("5.16.12-zen1-1-zen\n", res);
        }

        [Fact]
        public async void Distro_Bad()
        {
            await Assert.ThrowsAsync<PlatformInfoException>(() => _bad.Distro());
        }

        [Fact]
        public void Arch_Bad()
        {
            Assert.Throws<PlatformInfoException>(() => _bad.Arch());
        }

        [Fact]
        public void KernelVersion_Bad()
        {
            Assert.Throws<PlatformInfoException>(() => _bad.KernelVersion());
        }
   }
}
