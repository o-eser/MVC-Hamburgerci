using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Application.Models.VMs;
using Hamburgerci.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Hamburgerci.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MenuController : Controller
	{
		private readonly IMenuService _menuService;

		public MenuController(IMenuService menuService)
		{
			_menuService = menuService;
		}

		public async Task<IActionResult> Index(string searchText, int pageSize = 5, int page = 1)
		{
			MenuListingVM vm = new MenuListingVM();
			var menuler = await _menuService.GetAll();
			vm.Menuler = menuler.ToPagedList(pageNumber: page, pageSize: pageSize);

			if (!String.IsNullOrEmpty(searchText))
			{
				menuler = await _menuService.Search(searchText);
				vm.Menuler = menuler.ToPagedList(page, pageSize);
			}

			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> MenuEkle(MenuListingVM model)
		{
			if (ModelState.IsValid)
			{
				await _menuService.Create(model.Menu);
				return RedirectToAction("Index");
			}
			return View(model);
		}

		public async Task<IActionResult> MenuSil(int id)
		{
			await _menuService.Delete(id);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> MenuDuzenle(MenuDTO model)
		{
			if (ModelState.IsValid)
			{
				await _menuService.Update(model);
			}

			return RedirectToAction("Index");
		}
	}
}
