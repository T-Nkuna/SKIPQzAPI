using SKIPQzAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models.Time
{
    public class TimeComponentInterval
    {
      
        public TimeComponentInterval() { }
        public TimeComponentInterval(TimeComponent startTime, TimeComponent endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }
     
        public int WorkingDayId { get; set; }
        public int TimeComponentIntervalId{get;set;}

        public TimeComponent StartTime
        {
            get; set;
        }

        public TimeComponent EndTime
        {
            get; set;
        }

        public static TimeOfDay ClassifyHour(double hours)
        {
            return hours >= DayTimeInterval.Morning.StartTime.Hour && hours < DayTimeInterval.Morning.EndTime.Hour ? TimeOfDay.Morning : hours >= DayTimeInterval.AfterNoon.StartTime.Hour && hours < DayTimeInterval.AfterNoon.EndTime.Hour ? TimeOfDay.AfterNoon : hours >= DayTimeInterval.Evening.StartTime.Hour && hours < DayTimeInterval.Evening.EndTime.Hour ? TimeOfDay.Evening : TimeOfDay.Night;
        }

        public static TimeOfDay ClassifyTime(TimeComponent time)
        {
            return ClassifyHour(time.Hour);
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

        public static List<TimeComponent> GenerateTimeComponents(TimeComponent startTime, TimeComponent endTime, double minuteInterval)
        {
            var startingTimeComponent = new TimeComponent(startTime.Hour, startTime.Minute);
            var timeComponents = new List<TimeComponent>();
            while (startingTimeComponent.ToMinutes() < endTime.ToMinutes())
            {
                timeComponents.Add(startingTimeComponent);
                startingTimeComponent = startingTimeComponent.AddMinutes(minuteInterval);
            }

            return timeComponents;
        }

        public static List<TimeComponent> GenerateTimeComponents(TimeOfDay timeOfDay, double minuteInterval)
        {
            var returned = new List<TimeComponent>();

            switch (timeOfDay)
            {
                case TimeOfDay.Morning:
                    returned = GenerateTimeComponents(DayTimeInterval.Morning.StartTime.Hour, DayTimeInterval.Morning.EndTime.Hour, minuteInterval);
                    break;
                case TimeOfDay.AfterNoon:
                    returned = GenerateTimeComponents(DayTimeInterval.AfterNoon.StartTime.Hour, DayTimeInterval.AfterNoon.EndTime.Hour, minuteInterval);
                    break;
                case TimeOfDay.Evening:
                    returned = GenerateTimeComponents(DayTimeInterval.Evening.StartTime.Hour, DayTimeInterval.Evening.EndTime.Hour, minuteInterval);
                    break;
                default:
                    returned = GenerateTimeComponents(DayTimeInterval.Night.StartTime.Hour, DayTimeInterval.Night.EndTime.Hour, minuteInterval);
                    break;
            }

            return returned;
        }
    }
}
