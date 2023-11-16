using Hamburgerci.Entities.Concrete;

namespace Hamburgerci.UI.Models.ViewModels
{
    public class MenuEkstraMalzemeVM
    {
        public MenuEkstraMalzemeVM()
        {
            Menuler = new List<Menu>();
            EkstraMalzemeler = new List<EkstraMalzeme>();
        }
        public List<Menu> Menuler { get; set; }
        public List<EkstraMalzeme> EkstraMalzemeler { get; set; }
    }
}
