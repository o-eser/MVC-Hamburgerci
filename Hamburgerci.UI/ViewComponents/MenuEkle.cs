﻿using Hamburgerci.Application.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.ViewComponents
{
	public class MenuEkle: ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
