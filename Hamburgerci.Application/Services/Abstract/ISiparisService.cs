﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Application.Models.DTOs;

namespace Hamburgerci.Application.Services.Abstract
{
	public interface ISiparisService
	{
		Task Create(CreateSiparisDTO model);
		Task<CreateSiparisDTO> CreatePost();
		Task Update(UpdateSiparisDTO model);
		Task Delete(string id);
		Task<UpdateSiparisDTO> GetById(string id);
		Task<List<UpdateSiparisDTO>?> GetAll();
	}
}
