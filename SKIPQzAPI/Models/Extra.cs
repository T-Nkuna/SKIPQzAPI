using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models
{
    public class Extra
    {
        public int ExtraId { get; set; }

        public decimal Cost { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }
    }
}
