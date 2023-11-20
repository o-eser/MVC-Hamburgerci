using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Application.Models.VMs;

namespace Hamburgerci.Application.Services.Abstract
{
    public interface IEkstraMalzemeService
	{
		Task Create(EkstraMalzemeDTO model);
		Task Update(EkstraMalzemeDTO model);
		Task Delete(int id);
		Task<EkstraMalzemeDTO> GetById(int id);
		Task<List<EkstraMalzemeDTO>?> GetAll();
        Task<List<EkstraMalzemeDTO>?> Search(string searchText);
    }
}
