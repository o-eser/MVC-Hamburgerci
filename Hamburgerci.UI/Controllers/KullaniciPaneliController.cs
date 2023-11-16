using Hamburgerci.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.Controllers
{
    public class KullaniciPaneliController : Controller
    {
        public IActionResult Index()
        {
            SiparisVM model=new SiparisVM();
            return View(model);
        }
    }
}
