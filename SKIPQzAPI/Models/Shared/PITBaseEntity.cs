using SKIPQzAPI.Common.Constants;


namespace SKIPQzAPI.Models.Shared
{
    public interface IPITBaseEntity<T> : IBaseEntity<T>
    {
        public T MasterId { get; set; }

        public PITStatus Status { get; set; }
    }
    public class PITBaseEntity : BaseEntity, IPITBaseEntity<long?>
    {
        public long? MasterId { get ; set ; }
        public PITStatus Status { get ; set ; }
    }
}
