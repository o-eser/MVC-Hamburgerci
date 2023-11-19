
using Hamburgerci.Application.Models.VMs;
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
