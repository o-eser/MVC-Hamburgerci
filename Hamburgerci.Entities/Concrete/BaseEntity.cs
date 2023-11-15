using Hamburgerci.Entities.Abstract;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Entities.Concrete
{
    public class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
            DataStatus = DataStatus.Inserted;
        }
        public Kullanici CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Kullanici? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DataStatus DataStatus { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Kullanici? DeletedBy { get; set; }
    }
}
