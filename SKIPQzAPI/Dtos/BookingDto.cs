using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Dtos
{
    public class BookingDto
    {
        public ServiceProviderDto ServiceProvider { get; set; }
        public ServiceDto Service{get;set;}

        public string BookedDate { get; set; }

        public string StartTimeSlot { get; set; }

        public string EndTimeSlot { get; set; }

        public int BookingId { get; set; }
    }
}
