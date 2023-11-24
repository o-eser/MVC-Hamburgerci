using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Application.Services.Abstract;
using Hamburgerci.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.Controllers
{
    public class AppUserController : Controller
    {
        private readonly IAppUserService _kullaniciService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public AppUserController(IAppUserService kullaniciService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _kullaniciService = kullaniciService;
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email

                };
                try
                {
                    IdentityResult identityResult = await _userManager.CreateAsync(appUser, model.Password);
                    if (identityResult.Succeeded)
                        return RedirectToAction("Login");
                    else
                    {
                        foreach (IdentityError error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                

                
            }
            return View(model);
        }

        public IActionResult Login(string returnURL)
        {
            returnURL = returnURL is null ? "Index" : returnURL;
            return View(new LoginDTO()
            {
                ReturnURL = returnURL
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByNameAsync(model.UserName); ;
                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser.UserName, model.Password, false, false);

                    if (result.Succeeded)
                    {
						//// Kullanıcının rollerini yükle
						//var roles = await _userManager.GetRolesAsync(appUser);

						//// Kullanıcının rollerini cookie'ye ekle
						//foreach (var role in roles)
						//{
						//	// Cookie'ye rol ekleyerek, [Authorize(Roles = "Admin")] kontrolüne izin verilmiş olacak
						//	HttpContext.Response.Cookies.Append("UserRole", role, new CookieOptions { HttpOnly = false });
						//}

						return RedirectToAction("Index","Home");
                    }
                ModelState.AddModelError("", "Invalid Email or Password");
                }
            }
            return View(model);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
