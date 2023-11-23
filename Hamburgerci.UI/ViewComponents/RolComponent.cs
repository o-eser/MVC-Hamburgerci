using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.ViewComponents
{
    public class RolComponent : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
