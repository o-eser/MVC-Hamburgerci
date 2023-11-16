using Hamburgerci.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.Controllers
{
    public class YonetimPaneliController : Controller
    {
        IMenuService _menuService;
        IEkstraMalzemeService _ekstraMalzemeService;

        public YonetimPaneliController(IMenuService menuService, IEkstraMalzemeService ekstraMalzemeService)
        {
            _menuService = menuService;
            _ekstraMalzemeService = ekstraMalzemeService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
