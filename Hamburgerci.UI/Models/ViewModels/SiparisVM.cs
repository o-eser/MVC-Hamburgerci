using Hamburgerci.Entities.Concrete;

namespace Hamburgerci.UI.Models.ViewModels
{
    public class SiparisVM
    {
        public SiparisVM()
        {
            Siparisler = new List<Siparis>();
        }

        public List<Siparis> Siparisler { get; set; }

    }
}
