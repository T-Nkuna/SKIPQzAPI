
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models.Time
{
    public class WorkingDay
    {
        public int WorkingDayId
        {
            get;
            set;
        }

        public DayOfWeek WeekDay { get; set; }

        public int ServiceProviderId { get; set; }
        public List<TimeComponentInterval> Shifts { get; set; }
    }
}
