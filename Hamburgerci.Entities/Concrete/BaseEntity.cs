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
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DataStatus DataStatus { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
