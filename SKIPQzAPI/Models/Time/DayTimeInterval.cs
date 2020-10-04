using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models.Time
{
    public static class DayTimeInterval
    {
        public static TimeComponentInterval Morning
        {
            get
            {
                return new TimeComponentInterval(new TimeComponent(6,0), new TimeComponent(12,0));
            }
        }

        public static TimeComponentInterval AfterNoon
        {
            get
            {
                return new TimeComponentInterval(new TimeComponent(12,0), new TimeComponent(16,0));
            }
        }

        public static TimeComponentInterval Evening
        {
            get
            {
                return new TimeComponentInterval(new TimeComponent(16,0),new TimeComponent( 19,0));
            }
        }

        public static TimeComponentInterval Night
        {
            get
            {
                return new TimeComponentInterval(new TimeComponent(19,0), new TimeComponent(24,0));
            }
        }
    }
}
