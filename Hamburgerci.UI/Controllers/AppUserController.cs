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

        public AppUserController(IAppUserService kullaniciService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _kullaniciService = kullaniciService;
            _userManager = userManager;
            _signInManager = signInManager;
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
                        return Redirect(model.ReturnURL ?? "/");
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
