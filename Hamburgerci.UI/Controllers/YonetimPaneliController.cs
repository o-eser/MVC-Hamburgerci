
using Hamburgerci.Entities.Concrete;
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
            MenuEkstraMalzemeVM vm = new MenuEkstraMalzemeVM();
            vm.Menuler = _menuService.GetAll().ToList();
            vm.EkstraMalzemeler = _ekstraMalzemeService.GetAll().ToList();
            return View(vm);
        }

        public IActionResult MenuEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MenuEkleAsync(MenuVM model)
        {
            if (ModelState.IsValid)
            {
                await _menuService.CreateAsync(model.Menu);
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}
