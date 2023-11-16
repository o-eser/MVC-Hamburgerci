using Hamburgerci.Entities.Abstract;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Entities.Concrete
{
    public class EkstraMalzeme : BaseEntity, IEntity<int>
    {
        public int Id { get; set; }
        public string EkstraMalzemeAdi { get; set; }
        public double EkstraMalzemeFiyati { get; set; }
        public ParaBirimi ParaBirimi { get; set; }

        public ICollection<Siparis> Siparisler { get; set; }
    }
}
