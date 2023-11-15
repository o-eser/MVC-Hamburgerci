using Hamburgerci.Entities.Concrete;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Entities.Abstract
{
    public interface IBaseEntity
    {
        public Kullanici CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Kullanici? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Kullanici? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DataStatus DataStatus { get; set; }
    }
}
