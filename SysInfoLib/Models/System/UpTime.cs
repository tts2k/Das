namespace SysInfoLib
{
    public class Uptime
    {
        public int day {get; set;}
        public int hour {get; set;}
        public int min {get; set;}

        public Uptime(int day, int hour, int min) {
            this.day = day;
            this.hour = hour;
            this.min = min;
        }
    }
}
