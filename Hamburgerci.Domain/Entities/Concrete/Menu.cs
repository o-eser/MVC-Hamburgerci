using Hamburgerci.Entities.Abstract;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Entities.Concrete
{
    public class Menu : BaseEntity, IEntity<int>
    {
        public Menu()
        {
            Siparisler = new HashSet<Siparis>();
        }
        public int Id { get; set; }
        public string MenuAdi { get; set; }
        public double MenuFiyati { get; set; }
        public int Adet { get; set; }
        public ParaBirimi ParaBirimi { get; set; }

        public ICollection<Siparis> Siparisler { get; set; }
    }
}
