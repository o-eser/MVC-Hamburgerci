using Hamburgerci.Entities.Abstract;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Entities.Concrete
{
    public class EkstraMalzeme : IBaseEntity, IEntity<int>
    {
        public string EkstraMalzemeAdi { get; set; }
        public double EkstraMalzemeFiyati { get; set; }
        public ParaBirimi ParaBirimi { get; set; }
        public int Id { get; set; }
        public Kullanici CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Kullanici? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Kullanici? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DataStatus DataStatus { get; set; }
    }
}
