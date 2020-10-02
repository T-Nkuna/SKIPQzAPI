using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models
{
    public class TimeSlot
    {
        public int TimeSlotId { get; set; }

        public TimeOfDay TheTimeOfDay { get; set; }

        public DayOfWeek TheDayOfWeek { get; set; }
      
        public bool Recurring { get; set; }

        public DateTime Time { get; set; }

        public Schedule Schedule { get; set; }

        public string GetTimeSlot() => $"{Time.Hour.ToString()}:{Time.Minute.ToString()}";

        public DayOfWeek GetDayOfWeek() => Recurring ? TheDayOfWeek : Time.DayOfWeek;

        

    }

    public enum TimeOfDay { Morning,AfterNoon,Evening}
}
