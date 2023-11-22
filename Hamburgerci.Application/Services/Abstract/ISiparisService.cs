using System;
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
		Task<CreateSiparisDTO> CreateSiparis();
		Task Update(UpdateSiparisDTO model);
		Task Delete(int id);
		Task<UpdateSiparisDTO> GetById(int id);
		Task<List<UpdateSiparisDTO>?> GetAll();
        Task<List<UpdateSiparisDTO>?> Search(string searchText);
    }
}
