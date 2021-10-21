
using System.ComponentModel.DataAnnotations;


namespace SKIPQzAPI.Models
{
    public class Extra
    {
        public int ExtraId { get; set; }

        public decimal Cost { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        public double Duration { get; set; }
    }
}
