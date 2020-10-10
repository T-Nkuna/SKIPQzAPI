using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Dtos
{
    public class WorkingDayDto
    {
        public DayOfWeek DayOfWeek
        {
            get;set;
        }

        public List<TimeInterval> Shifts
        {
            get;set;
        }
    }

    public class TimeInterval
    {
        public string StartTimeSlot
        {
            get;
            set;
        }

        public string EndTimeSlot
        {
            get;set;
        }
        
    }
}
