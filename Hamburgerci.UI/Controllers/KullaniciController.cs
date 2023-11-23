using Hamburgerci.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly UserManager<Kullanici> _userManager;

        public KullaniciController(UserManager<Kullanici> userManager)
        {
            _userManager = userManager;

        }

        public IActionResult Index()
        {
            return View(_userManager.Users);
        }
    }
}
