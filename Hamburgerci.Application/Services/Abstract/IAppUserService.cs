﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Application.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Hamburgerci.Application.Services.Abstract
{
    public interface IAppUserService
	{
		Task<IdentityResult> Register(RegisterDTO model);
		Task<SignInResult> Login(LoginDTO model);
		Task<UpdateProfileDTO> GetByUserName(string userName);
		Task UpdateProfile(UpdateProfileDTO model);
		Task Logout();
	}
}
