
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

        public async Task<IActionResult> MenuDuzenle(int id)
        {
            MenuVM vm = new MenuVM();
            vm.Menu = await _menuService.GetByIdAsync(id);
            return View(vm);
        }

        [HttpPost]
        public IActionResult MenuDuzenle(MenuVM model)
        {
            if (ModelState.IsValid)
            {
                _menuService.Update(model.Menu);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> MenuSil(int id)
        {
            await _menuService.RemoveAsync(id);
            return RedirectToAction("Index");
        }
        public IActionResult EkstraMalzemeEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EkstraMalzemeEkle(EkstraMalzemeVM model)
        {
            if (ModelState.IsValid) 
            {
                await _ekstraMalzemeService.CreateAsync(model.EkstraMalzeme);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> EkstraMalzemeDuzenle(int id)
        {
            EkstraMalzemeVM vm = new EkstraMalzemeVM();
            vm.EkstraMalzeme = await _ekstraMalzemeService.GetByIdAsync(id);
            return View(vm);
        }

        [HttpPost]
        public IActionResult EkstraMalzemeDuzenle(EkstraMalzemeVM model)
        {
            if (ModelState.IsValid)
            {
                _ekstraMalzemeService.Update(model.EkstraMalzeme);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> EkstraMalzemeSil(int id)
        {
            await _ekstraMalzemeService.RemoveAsync(id);
            return RedirectToAction("Index");
        }




    }
}
