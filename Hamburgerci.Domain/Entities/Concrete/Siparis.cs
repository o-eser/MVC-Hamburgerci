using Hamburgerci.Domain.Entities.Concrete;
using Hamburgerci.Entities.Abstract;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Entities.Concrete
{
    public class Siparis : BaseEntity, IEntity<int>
    {
        public int Id { get; set; }
        public MenuBoyutu MenuBoyutu { get; set; }
        public int SiparisAdeti { get; set; }
        public double? ToplamTutar { get; set; }

        public ICollection<MenuSiparis>? MenuSiparisler { get; set; }

        public ICollection<EkstraMalzemeSiparis>? EkstraMalzemeSiparisler { get; set; }
        public int? KullaniciId { get; set; }
        public Kullanici? Kullanici { get; set; }

    }
}
