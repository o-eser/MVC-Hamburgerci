﻿using Hamburgerci.UI.Models.ViewModels;
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
