using Hamburgerci.Entities.Abstract;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Entities.Concrete
{
    public class Siparis : IBaseEntity, IEntity<Guid>
    {
        public Guid Id { get; set; }
        public MenuBoyutu MenuBoyutu { get; set; }
        public int SiparisAdeti { get; set; }
        public double ToplamTutar { get; }
        public Kullanici CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Kullanici? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Kullanici? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DataStatus DataStatus { get; set; }

        public ICollection<Menu> Menuler { get; set; }

        public ICollection<EkstraMalzeme> EkstraMalzemeler { get; set; }

    }
}
