using Hamburgerci.Entities.Concrete;
using Hamburgerci.Repositories.Abstract;
using Hamburgerci.Services.Abstract;
using Hamburgerci.UI.Models.ViewModels;
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
            MenuEkstraMalzemeVM model = new MenuEkstraMalzemeVM();
            model.Menuler = _menuService.GetAll().ToList();
            model.EkstraMalzemeler=_ekstraMalzemeService.GetAll().ToList();
            return View(model);
        }
    }
}
