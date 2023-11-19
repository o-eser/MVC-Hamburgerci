using Hamburgerci.Entities.Concrete;

namespace Hamburgerci.UI.Models.ViewModels
{
    public class EkstraMalzemeVM
    {
        public EkstraMalzemeVM()
        {
            EkstraMalzemeler = new List<EkstraMalzeme>();
        }
        public EkstraMalzeme EkstraMalzeme { get; set; }

        public List<EkstraMalzeme> EkstraMalzemeler { get; set; }
    }
}
