using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Application.Services.Abstract;
using Hamburgerci.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.Controllers
{
    public class AppUserController : Controller
    {
        private readonly IAppUserService _kullaniciService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly IEmailSender _emailSender;

        public AppUserController(IAppUserService kullaniciService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> passwordHasher,IEmailSender emailSender)
        {
            _kullaniciService = kullaniciService;
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _emailSender = emailSender;
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
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                        var confirmationLink = Url.Action("ConfirmEmail", "AppUser", new { userId = appUser.Id, token = token }, Request.Scheme);

                        // E-posta gönderme işlemi
                        await _emailSender.SendEmailAsync(appUser.Email, "E-posta Doğrulama", $"Lütfen hesabınızı doğrulamak için linke <a href='{confirmationLink}'>tıklayın</a>.");

                        return RedirectToAction("Login");
                    }
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
                AppUser appUser = await _userManager.FindByNameAsync(model.UserName);
                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser.UserName, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Siparis");
                    }
                    ModelState.AddModelError("", "Invalid Email or Password");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login","appuser");
        }

        public IActionResult ErisimEngellendi()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Error", "HoAppUserme");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Error", "AppUser");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                // E-posta doğrulama başarılı
                return View("ConfirmEmail");
            }
            else
            {
                // E-posta doğrulama başarısız
                return RedirectToAction("Error", "AppUser");
            }
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
