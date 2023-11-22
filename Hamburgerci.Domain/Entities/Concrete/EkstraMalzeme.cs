using Hamburgerci.Domain.Entities.Concrete;
using Hamburgerci.Entities.Abstract;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Entities.Concrete
{
    public class EkstraMalzeme : BaseEntity, IEntity<int>
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public double Fiyati { get; set; }
        public ParaBirimi ParaBirimi { get; set; }

        public ICollection<EkstraMalzemeSiparis> EkstraMalzemeSiparisler { get; set; }
    }
}
