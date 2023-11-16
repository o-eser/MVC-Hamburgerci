using Hamburgerci.Entities.Concrete;

namespace Hamburgerci.UI.Models
{
    public class MenuEkstraMalzemeVM
    {
        public MenuEkstraMalzemeVM()
        {
            Menuler = new List<Menu>();
            EkstraMalzemeler = new List<EkstraMalzeme>();
        }
        List<Menu> Menuler { get; set; }
        List<EkstraMalzeme> EkstraMalzemeler { get; set; }
    }
}
