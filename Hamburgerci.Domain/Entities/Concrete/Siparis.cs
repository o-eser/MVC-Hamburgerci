using Hamburgerci.Entities.Abstract;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Entities.Concrete
{
    public class Siparis : BaseEntity, IEntity<string>
    {
        public string Id { get; set; }
        public MenuBoyutu MenuBoyutu { get; set; }
        public int SiparisAdeti { get; set; }
        public double ToplamTutar { get; set; }

        public ICollection<Menu> Menuler { get; set; }

        public ICollection<EkstraMalzeme> EkstraMalzemeler { get; set; }
        public int KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }

    }
}
