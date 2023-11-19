using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Application.Services.Abstract;
using Hamburgerci.Entities.Concrete;
using Hamburgerci.Entities.Enum;
using Hamburgerci.Repositories.Abstract;

namespace Hamburgerci.Application.Services.Concrete
{
	public class MenuService : IMenuService
	{
		private readonly IMenuRepository _menuRepository;

		public MenuService(IMenuRepository menuRepository)
		{
			_menuRepository = menuRepository;
		}

		public async Task Create(MenuDTO model)
		{
			Menu ekstraMalzeme = new Menu
			{
				MenuAdi = model.MenuAdi,
				MenuFiyati = model.MenuFiyati,
				ParaBirimi = model.ParaBirimi
			};


			await _menuRepository.Create(ekstraMalzeme);
		}

		public async Task Delete(int id)
		{
			Menu menu = await _menuRepository.GetDefault(g => g.Id == id);

			if (id == 0)
			{
				throw new ArgumentException("Id 0 Olamaz!");

			}
			else if (menu == null)
			{
				throw new ArgumentException("Böyle bir menu mevcut değil!");
			}

			menu.DataStatus = DataStatus.Deleted;
			menu.DeletedDate = DateTime.Now;

			await _menuRepository.Delete(menu);
		}

		public async Task<MenuDTO> GetById(int id)
		{
			return await _menuRepository.GetFilteredFirstOrDefault(x => new MenuDTO
			{
				MenuAdi= x.MenuAdi,
				MenuFiyati = x.MenuFiyati,
				ParaBirimi = x.ParaBirimi,
				Id = x.Id,
			}, g => g.Id == id && g.DataStatus != DataStatus.Deleted);
		}

		public async Task Update(MenuDTO model)
		{
			Menu menu = new Menu();

			menu = await _menuRepository.GetDefault(g => g.Id == model.Id);

			if (menu == null)
			{
				throw new Exception("Böyle bir menü mevcut değil!");
			}

			menu.MenuAdi = model.MenuAdi;
			menu.MenuFiyati = model.MenuFiyati;
			menu.ParaBirimi = model.ParaBirimi;
			menu.ModifiedDate = DateTime.Now;
			menu.DataStatus = DataStatus.Updated;

			await _menuRepository.Update(menu);

		}

		public async Task<List<MenuDTO>?> GetAll()
		{
			return await _menuRepository.GetFilteredList(x => new MenuDTO
			{
				MenuAdi = x.MenuAdi,
				MenuFiyati = x.MenuFiyati,
				ParaBirimi = x.ParaBirimi,
				Id = x.Id,
			}, g => g.DataStatus != DataStatus.Deleted) as List<MenuDTO>;
		}

		public async Task<List<MenuDTO>> Search(string searchText)
		{
			return await _menuRepository.GetFilteredList(x => new MenuDTO
			{
				MenuAdi = x.MenuAdi,
				MenuFiyati = x.MenuFiyati,
				ParaBirimi = x.ParaBirimi,
				Id = x.Id,
			}, g => g.MenuAdi.Contains(searchText) && g.DataStatus != DataStatus.Deleted) as List<MenuDTO>;
		}
	}
}
