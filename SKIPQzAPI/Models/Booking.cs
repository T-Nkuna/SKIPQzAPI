using Microsoft.AspNetCore.Identity;
using SKIPQzAPI.Common.Constants;
using SKIPQzAPI.Models.Shared;
using SKIPQzAPI.Models.Time;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models
{
    public class Booking : PITBaseEntity
    {

        public long? ServiceId { get; set; }

        public long? ServiceProviderId { get; set; }

        public DateTime BookedDate { get; set; }

        public TimeComponentInterval BookedTimeInterval { get; set; }

        public List<Extra> Extras { get; set; }

        public IdentityUser client { get; set; }

        public decimal Cost { get; set; }


        public bool CanCancel
        {
            get
            {
                
                var bookingTime = new DateTime(this.BookedDate.Year, this.BookedDate.Month, this.BookedDate.Day, (int)(this.BookedTimeInterval?.StartTime?.Hour??0), (int)(this.BookedTimeInterval?.StartTime?.Minute??0),0);
                var dateTimeDiff = bookingTime.Subtract(DateTime.Now);
                var hoursToBooking = dateTimeDiff.Hours + dateTimeDiff.Days*24 + (dateTimeDiff.Minutes/60);
                return hoursToBooking >=2;
            }
        }
      
    }
}
