using System.Security.Claims;
using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Application.Services.Abstract;
using Hamburgerci.Entities.Concrete;
using Hamburgerci.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Hamburgerci.Application.Services.Concrete
{
    public class AppUserService : IAppUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IAppUserRepository _kullaniciRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;


		public AppUserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAppUserRepository kullaniciRepository, IHttpContextAccessor httpContextAccessor)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_kullaniciRepository = kullaniciRepository;
			_httpContextAccessor = httpContextAccessor;
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

		public async Task<int> GetUserId()
		{
			AppUser currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
			return currentUser.Id;
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
			AppUser user = new AppUser
			{
				UserName = model.UserName,
				Email = model.Email
			};

			IdentityResult result = await _userManager.CreateAsync(user, model.Password);
			if (result.Succeeded)
				await _signInManager.SignInAsync(user, isPersistent: false);
			return result;
		}

		public async Task UpdateProfile(UpdateProfileDTO model)
		{
			AppUser user = await _kullaniciRepository.GetDefault(x => x.Id == model.Id);

			if (model.Password != null)
			{
				user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);

				await _userManager.UpdateAsync(user);
			}

			if (model.Email != null)
			{
				AppUser isUserEmailExist = await _userManager.FindByEmailAsync(model.Email);

				if (isUserEmailExist == null)
					await _userManager.SetEmailAsync(user, model.Email);
			}
		}

		
	}
}
