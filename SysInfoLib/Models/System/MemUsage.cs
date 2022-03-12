namespace SysInfoLib
{
    public class MemUsage
    {
        public long total {get; set;}
        public long used {get; set;}

        public MemUsage(long total, long used) {
            this.total = total;
            this.used = used;
        }
    }
}
