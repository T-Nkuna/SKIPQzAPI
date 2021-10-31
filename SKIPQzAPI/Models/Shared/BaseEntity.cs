using System;

namespace SKIPQzAPI.Models.Shared
{
    public interface IBaseEntity<T>
    {
        public bool Inactive { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }

        public T Id { get; set; }

    }

    public class BaseEntity : IBaseEntity<long?>
    {
        public bool Inactive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public long? Id { get; set; }
        public long? OrganisationId { get; set; }
    }
}
