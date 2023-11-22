using System.Diagnostics;
using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Application.Services.Abstract;
using Hamburgerci.Entities.Enum;
using Hamburgerci.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISiparisService _siparisService;
        private readonly ILogger<HomeController> _logger;


		public HomeController(ILogger<HomeController> logger, ISiparisService siparisService)
		{
			_logger = logger;
			_siparisService = siparisService;
		}

		public async Task<IActionResult> Index()
        {
            //CreateSiparisDTO model = new CreateSiparisDTO();
            //model.MenuBoyutu = MenuBoyutu.Buyuk;
            //model.SiparisAdeti = 2;
            //model.MenuSiparisler = new List<MenuSiparisDTO> { new MenuSiparisDTO { MenuAdeti = 2, MenuId = 1 } };

            //await _siparisService.Create(model);

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