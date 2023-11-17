using Hamburgerci.Entities.Concrete;
using X.PagedList;

namespace Hamburgerci.UI.Models.ViewModels
{
    public class MenuVM
    {
        public MenuVM()
        {
            Menuler = new PagedList<Menu>(null, 1, 1);
        }
        public Menu Menu { get; set; }
        public IPagedList<Menu> Menuler { get; set; }
    }
}
