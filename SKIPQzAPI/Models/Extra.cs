
using SKIPQzAPI.Models.Shared;
using System.ComponentModel.DataAnnotations;


namespace SKIPQzAPI.Models
{
    public class Extra : BaseEntity
    {
        public decimal Cost { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        public double Duration { get; set; }
    }
}
