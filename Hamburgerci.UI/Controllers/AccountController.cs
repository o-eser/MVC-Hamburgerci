using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Entities.Concrete;
using Hamburgerci.UI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AccountController : Controller
    {
        //Dependency Injection
        private readonly UserManager<Kullanici> _userManager;
        private readonly SignInManager<Kullanici> _signInManager;
        private readonly IPasswordHasher<Kullanici> _passwordHasher;

        public AccountController(UserManager<Kullanici> userManager, SignInManager<Kullanici> signInManager, IPasswordHasher<Kullanici> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();

        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                Kullanici kullanici = new Kullanici
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                IdentityResult identityResult = await _userManager.CreateAsync(kullanici, model.Password);


                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Login", "Account"); 
                }
                else 
                {
                    foreach (IdentityError error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            returnUrl = returnUrl is null ? "Index" : returnUrl; 
            return View(new LoginVM
            {
                ReturnUrl = returnUrl
            });

        }


        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                Kullanici kullanici = await _userManager.FindByNameAsync(model.UserName);

                if (kullanici != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(kullanici.UserName, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        //return Redirect(model.ReturnUrl ?? "/Role/Index"); --> bu çalışmadı
                        return Redirect(Url.Action("Index", "Role"));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Wrong credantion information...");
                    }
                }

            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");

        }

        public async Task<IActionResult> Edit()
        {
            Kullanici appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            UpdateProfileDTO updateProfileDTO = new UpdateProfileDTO()
            {
                Email = appUser.Email,
                UserName = appUser.UserName,
                Password = appUser.PasswordHash

            };


            return View(updateProfileDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfileDTO model)
        {
            if (ModelState.IsValid)
            {
                Kullanici kullanici = await _userManager.FindByNameAsync(User.Identity.Name);

                kullanici.UserName = model.UserName;


                if (model.Password != null)
                {
                    kullanici.PasswordHash = _passwordHasher.HashPassword(kullanici, model.Password);
                }

                IdentityResult result = await _userManager.UpdateAsync(kullanici);

                if (result.Succeeded)
                {
                    TempData["Success"] = "The user information has been updated successfully";
                }
                else
                {
                    TempData["Error"] = "The user information has not been updated";
                }

            }
            return View(model);
        }
    }
}
