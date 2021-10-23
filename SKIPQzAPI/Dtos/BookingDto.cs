using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Dtos
{
    public class BookingDto
    {
        public int ServiceProviderId { get; set; }
        public int ServiceId{get;set;}

        public List<int> ExtraIds { get; set; }

        public string BookedDate { get; set; }

        public string StartTimeSlot { get; set; }

        public string EndTimeSlot { get; set; }

        public int BookingId { get; set; }

        public string UserName { get; set; } //client

        public decimal Cost { get; set; }

        public bool CanCancel { get; set; }
    }
}
