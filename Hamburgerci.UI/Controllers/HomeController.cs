using System.Diagnostics;
using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Application.Services.Abstract;
using Hamburgerci.Entities.Concrete;
using Hamburgerci.Entities.Enum;
using Hamburgerci.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.Controllers
{
	[Authorize]
	public class HomeController : Controller
    {
        private readonly ISiparisService _siparisService;
        private readonly ILogger<HomeController> _logger;
		private readonly RoleManager<IdentityRole<int>> _roleManager;
		private readonly UserManager<AppUser> userManager;

		public HomeController(ILogger<HomeController> logger, ISiparisService siparisService, RoleManager<IdentityRole<int>> roleManager, UserManager<AppUser> userManager)
		{
			_logger = logger;
			_siparisService = siparisService;
			_roleManager = roleManager;
			this.userManager = userManager;
		}

		public async Task<IActionResult> Index()
        {

			// Role Oluşturma
			//string[] roleNames = { "Admin", "User", "Editor" };

			//IdentityResult roleResult;

			//foreach (var roleName in roleNames)
			//{
			//	var roleExist = await _roleManager.RoleExistsAsync(roleName);

			//	if (!roleExist)
			//	{
			//		// Rolü oluştur
			//		roleResult = await _roleManager.CreateAsync(new IdentityRole<int>(roleName));
			//	}
			//}

			//var user = await userManager.FindByNameAsync("osman");

			//if (user != null)
			//{
			//	await userManager.AddToRoleAsync(user, "Admin");
			//}

			return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}