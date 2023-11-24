using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Entities.Concrete;
using Hamburgerci.UI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.Controllers
{
    [Authorize(Roles = "admin")]
    public class RolController : Controller
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<Kullanici> _kullaniciManager;

        public RolController(RoleManager<IdentityRole<Guid>> roleManager, UserManager<Kullanici> kullaniciManager)
        {
            _roleManager = roleManager;
            _kullaniciManager = kullaniciManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.Select(x => new { x.Id, x.Name }).ToList(); //Rolleri veri tabanından çekiyoruz.

            List<RolVM> model = new List<RolVM>();
            foreach (var item in roles)
            {
                model.Add(new RolVM() { ID = item.Id, Name = item.Name });
            }
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RolVM model)
        {
            if (ModelState.IsValid) 
            {
                IdentityRole<Guid> role = await _roleManager.FindByNameAsync(model.Name); 

                if (role == null) 
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole<Guid>() { Name = model.Name }); 

                    if (result.Succeeded) 
                    {
                        TempData["Succeeded"] = $"{model.Name} isimli rol başarıyla eklendi";
                        return RedirectToAction("Index"); 
                    }
                    else 
                    { 
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                            TempData["Error"] = error.Description;
                        }
                    }

                }
                else 
                {
                    TempData["Message"] = $"{model.Name} isimli rol zaten mevcut";
                }
            }

            return View(model); 
        }


        public async Task<IActionResult> AssignedUser(string id)
        {
            IdentityRole<Guid> role = await _roleManager.FindByIdAsync(id);

            List<Kullanici> hasRole = new List<Kullanici>(); //kimlerin bu role sahip olduğunu tutacak liste
            List<Kullanici> hasNotRole = new List<Kullanici>();

            foreach (Kullanici user in _kullaniciManager.Users)
            {
                var list = await _kullaniciManager.IsInRoleAsync(user, role.Name) ? hasRole : hasNotRole;
                list.Add(user);
            }

            AssignedRoleDTO assignedRoleDTO = new AssignedRoleDTO()
            {
                Role = role,
                HasRole = hasRole,
                HasNotRole = hasNotRole,
                RoleName = role.Name
            };

            return View(assignedRoleDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AssignedUser(AssignedRoleDTO model)
        {


            foreach (string userId in model.AddIds ?? new string[] { })
            {
                Kullanici user = await _kullaniciManager.FindByIdAsync(userId);
                await _kullaniciManager.AddToRoleAsync(user, model.RoleName);
            }

            foreach (string userId in model.DeleteIds ?? new string[] { })
            {
                Kullanici user = await _kullaniciManager.FindByIdAsync(userId);
                await _kullaniciManager.RemoveFromRoleAsync(user, model.RoleName);
            }


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole<Guid> role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    TempData["Succeeded"] = $"{role.Name} isimli rol başarıyla silindi";
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        TempData["Error"] = error.Description;
                    }
                }
            }
            else
            {
                TempData["Message"] = $"{role.Name} isimli rol bulunamadı";
            }

            return RedirectToAction("Index");


        }
    }
}
