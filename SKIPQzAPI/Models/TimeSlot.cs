using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models
{
    public enum TimeOfDay { Morning, AfterNoon, Evening, Night }
    public class TimeSlot
    {
        public int TimeSlotId { get; set; }

        public TimeOfDay TheTimeOfDay { get; set; }

        public DayOfWeek TheDayOfWeek { get; set; }
      
        public string TimeSlotString
        {
            get;
            set;
        }
        public Schedule Schedule { get; set; } 

        public static double Interval {get; set;} = 15;

        public void SetTheTimeOfDayAndTimeSlotString(TimeComponent time)
        {
            TheTimeOfDay = TimeComponentInterval.ClassifyHour(time.Hour);
            TimeSlotString = time.ToString();
        }
    }

    public class TimeComponent
    {
        public TimeComponent(double hour, double minute)
        {
            Hour = hour;
            Minute = minute;
        }

        public double Hour { get; set; }

        public double Minute { get; set; }

        public double ToMinutes()
        {
           
            return Hour * 60 + Minute;
            
        }

        public static TimeComponent ToTimeComponent(double minutes){

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

    public class TimeComponentInterval
    {
        public TimeComponentInterval(double startHours,double endHours)
        {
            StartHours = startHours;
            EndHours = endHours;
        }

        public double StartHours { get; set; }

        public double EndHours { get; set; }

        public static TimeOfDay ClassifyHour(double hours)
        {
            return hours >= DayTimeInterVal.Morning.StartHours && hours < DayTimeInterVal.Morning.EndHours ? TimeOfDay.Morning : hours >= DayTimeInterVal.AfterNoon.StartHours && hours < DayTimeInterVal.AfterNoon.EndHours ? TimeOfDay.AfterNoon : hours >= DayTimeInterVal.Evening.StartHours && hours < DayTimeInterVal.Evening.EndHours ? TimeOfDay.Evening:TimeOfDay.Night;
        }

        public static List<TimeComponent> GenerateTimeComponents(double startHours, double endHours, double minuteInterval)
        {
            var startingTimeComponent = new TimeComponent(startHours, 0);
            var timeComponents = new List<TimeComponent>();
            while (startingTimeComponent.ToMinutes() < endHours * 60)
            {
                timeComponents.Add(startingTimeComponent);
                startingTimeComponent = startingTimeComponent.AddMinutes(minuteInterval);
            }

            return timeComponents;
        }

        public static List<TimeComponent> GenerateTimeComponents(TimeOfDay timeOfDay,double minuteInterval)
        {
            var returned = new List<TimeComponent>();

            switch (timeOfDay)
            {
                case TimeOfDay.Morning:
                    returned = GenerateTimeComponents(DayTimeInterVal.Morning.StartHours, DayTimeInterVal.Morning.EndHours, minuteInterval);
                    break;
                case TimeOfDay.AfterNoon:
                    returned = GenerateTimeComponents(DayTimeInterVal.AfterNoon.StartHours, DayTimeInterVal.AfterNoon.EndHours, minuteInterval);
                    break;
                case TimeOfDay.Evening:
                    returned = GenerateTimeComponents(DayTimeInterVal.Evening.StartHours, DayTimeInterVal.Evening.EndHours, minuteInterval);
                    break;
                default:
                    returned = GenerateTimeComponents(DayTimeInterVal.Night.StartHours, DayTimeInterVal.Night.EndHours,minuteInterval);
                    break;
            }

            return returned;
        }
    }

   public static class DayTimeInterVal
    {
        public static TimeComponentInterval Morning 
        { 
            get 
            {
                return new TimeComponentInterval(6,12); 
            }
        }

        public static TimeComponentInterval AfterNoon
        {
            get
            {
                return new TimeComponentInterval(12, 16);
            }
        }

        public static TimeComponentInterval Evening
        {
            get
            {
                return new TimeComponentInterval(16, 19);
            }
        }

        public static TimeComponentInterval Night
        {
            get
            {
                return new TimeComponentInterval(19, 24);
            }
        }
    }
}
