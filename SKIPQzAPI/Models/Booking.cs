using SKIPQzAPI.Models.Time;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int ServiceId { get; set; }

        public int ServiceProviderId { get; set; }

        public DateTime BookedDate { get; set; }

        public TimeComponentInterval BookedTimeInterval { get; set; }
    }
}
