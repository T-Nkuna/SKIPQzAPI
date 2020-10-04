using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models.Time
{
    public enum TimeOfDay { Morning, AfterNoon, Evening, Night }
    public class TimeComponent
    {
        public TimeComponent(double hour, double minute)
        {
            Hour = hour;
            Minute = minute;
        }

        public double Hour { get; set; }

        public double Minute { get; set; }

        public int TimeComponentId { get; set; }

        public double ToMinutes()
        {

            return Hour * 60 + Minute;

        }

        public static TimeComponent ToTimeComponent(double minutes)
        {

            double hours = (int)(minutes / 60);
            double mins = (minutes % 60);

            return new TimeComponent(hours, mins);
        }

        public TimeComponent AddMinutes(double minutes)
        {
            return ToTimeComponent(ToMinutes() + minutes);
        }

        public override string ToString()
        {
            var minStr = Minute.ToString().Length < 2 ? "0" + Minute : Minute.ToString();
            return $"{Hour}:{minStr}";
        }

    }
}
