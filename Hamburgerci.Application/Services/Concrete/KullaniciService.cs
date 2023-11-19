using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Application.Services.Abstract;
using Hamburgerci.Entities.Concrete;
using Hamburgerci.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Hamburgerci.Application.Services.Concrete
{
    public class KullaniciService : IKullaniciService
	{
		private readonly UserManager<Kullanici> _userManager;
		private readonly SignInManager<Kullanici> _signInManager;
		private readonly IKullaniciRepository _kullaniciRepository;

		public KullaniciService(UserManager<Kullanici> userManager, SignInManager<Kullanici> signInManager, IKullaniciRepository kullaniciRepository)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_kullaniciRepository = kullaniciRepository;
		}
		public async Task<UpdateProfileDTO> GetByUserName(string userName)
		{
			UpdateProfileDTO result = await _kullaniciRepository.GetFilteredFirstOrDefault(
				x => new UpdateProfileDTO
				{
					Id = x.Id,
					UserName = x.UserName,
					Email = x.Email,
					Password = x.PasswordHash
				},
				   x => x.UserName == userName);

			return result;
		}

		public async Task<SignInResult> Login(LoginDTO model)
		{
			return await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
		}

		public async Task Logout()
		{
			await _signInManager.SignOutAsync();
		}

		public async Task<IdentityResult> Register(RegisterDTO model)
		{
			Kullanici user = new Kullanici
			{
				UserName = model.UserName,
				Email = model.Email,
				CreatedDate = DateTime.Now
			};

			IdentityResult result = await _userManager.CreateAsync(user, model.Password);
			if (result.Succeeded)
				await _signInManager.SignInAsync(user, isPersistent: false);
			return result;
		}

		public async Task UpdateProfile(UpdateProfileDTO model)
		{
			Kullanici user = await _kullaniciRepository.GetDefault(x => x.Id == model.Id);

			if (model.Password != null)
			{
				user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);

				await _userManager.UpdateAsync(user);
			}

			if (model.Email != null)
			{
				Kullanici isUserEmailExist = await _userManager.FindByEmailAsync(model.Email);

				if (isUserEmailExist == null)
					await _userManager.SetEmailAsync(user, model.Email);
			}
		}
	}
}
