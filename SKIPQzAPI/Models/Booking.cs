using Microsoft.AspNetCore.Identity;
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

        public List<Extra> Extras { get; set; }

        public IdentityUser client { get; set; }

        public decimal Cost { get; set; }
      
    }
}
