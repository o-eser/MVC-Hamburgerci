using Hamburgerci.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.Controllers
{
    public class YonetimPaneliController : Controller
    {
        
        public IActionResult Index()
        {
            MenuEkstraMalzemeVM model = new MenuEkstraMalzemeVM();
            return View(model);
        }

    }
}
