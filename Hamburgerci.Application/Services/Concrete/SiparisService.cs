using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Application.Models.VMs;
using Hamburgerci.Application.Services.Abstract;
using Hamburgerci.Domain.Entities.Concrete;
using Hamburgerci.Domain.Repositories;
using Hamburgerci.Entities.Concrete;
using Hamburgerci.Entities.Enum;
using Hamburgerci.Repositories.Abstract;

namespace Hamburgerci.Application.Services.Concrete
{
	public class SiparisService : ISiparisService
	{
		private readonly ISiparisRepository _siparisRepository;
		private readonly IMenuRepository _menuRepository;
		private readonly IEkstraMalzemeRepository _ekstraMalzemeRepository;

		private readonly IMenuSiparisRepository _msRepository;
		private readonly IEkstraMalzemeRepository _esRepository;

		public SiparisService(ISiparisRepository siparisRepository, IMenuRepository menuRepository, IEkstraMalzemeRepository ekstraMalzemeRepository, IMenuSiparisRepository menuSiparisRepository, IEkstraMalzemeRepository esRepository)
		{
			_siparisRepository = siparisRepository;
			_menuRepository = menuRepository;
			_ekstraMalzemeRepository = ekstraMalzemeRepository;
			_msRepository = menuSiparisRepository;
			_esRepository = esRepository;
		}


		public async Task Create(CreateSiparisDTO model)
		{
			Guid id = Guid.NewGuid();
			Siparis siparis = new Siparis
			{
				MenuBoyutu = model.MenuBoyutu,
				SiparisAdeti = model.SiparisAdeti,
				ToplamTutar = model.ToplamTutar,
				CreatedDate = DateTime.Now,
				DataStatus = DataStatus.Inserted
			};

			await _siparisRepository.Create(siparis);


			List<MenuSiparis> menuSiparisler = new List<MenuSiparis>();

			foreach (var item in model.MenuSiparisler)
			{
				MenuSiparis ms = new MenuSiparis
				{
					MenuId = item.MenuId,
					SiparisId = siparis.Id,
					Adet = item.MenuAdeti,
					DataStatus = DataStatus.Inserted,
					CreatedDate = DateTime.Now
				};
				await _msRepository.Create(ms);

				menuSiparisler.Add(ms);
			}

			siparis.MenuSiparisler = menuSiparisler;

			List<EkstraMalzemeSiparis> ekstraMalzemeSiparisler = new List<EkstraMalzemeSiparis>();
			foreach (var item in model.EkstraMalzemeSiparisler)
			{
				EkstraMalzemeSiparis em = new EkstraMalzemeSiparis
				{
					EkstraMalzemeId = item.EkstraMalzemeId,
					SiparisId = siparis.Id,
					Adet = item.EkstraMalzemeAdeti,
					DataStatus = DataStatus.Inserted,
					CreatedDate = DateTime.Now
				};
				ekstraMalzemeSiparisler.Add(em);
			}

			siparis.EkstraMalzemeSiparisler = ekstraMalzemeSiparisler;

			await _siparisRepository.Update(siparis);
		}

		public async Task<CreateSiparisDTO> CreateSiparis()
		{
			CreateSiparisDTO createSiparis = new CreateSiparisDTO
			{
				EkstraMalzemeler = await _ekstraMalzemeRepository.GetFilteredList(x => new EkstraMalzemeVM
				{
					Id = x.Id,
					Adi = x.Adi,
					Fiyati = x.Fiyati,
					ParaBirimi = x.ParaBirimi
				},
				   x => x.DataStatus != DataStatus.Deleted) as List<EkstraMalzemeVM>,

				Menuler = await _menuRepository.GetFilteredList(x => new MenuVM
				{
					Id = x.Id,
					MenuAdi = x.MenuAdi,
					MenuFiyati = x.MenuFiyati,
					ParaBirimi = x.ParaBirimi
				}, x => x.DataStatus != DataStatus.Deleted) as List<MenuVM>
			};

			return createSiparis;
		}

		public async Task Delete(int id)
		{
			Siparis siparis = await _siparisRepository.GetDefault(g => g.Id == id);

			if (id == null)
			{
				throw new ArgumentException("Id 0 Olamaz!");

			}
			else if (siparis == null)
			{
				throw new ArgumentException("Böyle bir sipariş mevcut değil!");
			}

			siparis.DataStatus = DataStatus.Deleted;
			siparis.DeletedDate = DateTime.Now;

			await _siparisRepository.Delete(siparis);
		}

		public async Task<List<UpdateSiparisDTO>?> GetAll()
		{
			var siparisler = await _siparisRepository.GetFilteredList(x => new UpdateSiparisDTO
			{

				MenuBoyutu = x.MenuBoyutu,
				SiparisAdeti = x.SiparisAdeti,
				ToplamTutar = x.ToplamTutar
			}, x => x.DataStatus != DataStatus.Deleted);

			return siparisler.ToList();
		}


		//Todo : Gözden geçirilecek
		public async Task<List<UpdateSiparisDTO>?> Search(string searchText)
		{
			return await _siparisRepository.GetFilteredList(x => new UpdateSiparisDTO
			{


				MenuBoyutu = x.MenuBoyutu,
				SiparisAdeti = x.SiparisAdeti,
				ToplamTutar = x.ToplamTutar
			}, x => x.DataStatus != DataStatus.Deleted && x.Id == Convert.ToInt32(searchText)) as List<UpdateSiparisDTO>;
		}

		public async Task<UpdateSiparisDTO> GetById(int id)
		{
			return await _siparisRepository.GetFilteredFirstOrDefault(x => new UpdateSiparisDTO
			{


				MenuBoyutu = x.MenuBoyutu,
				SiparisAdeti = x.SiparisAdeti,
				ToplamTutar = x.ToplamTutar
			}, x => x.DataStatus != DataStatus.Deleted && x.Id == id);
		}

		public async Task Update(UpdateSiparisDTO model)
		{
			Siparis siparis = new Siparis();

			siparis = await _siparisRepository.GetDefault(g => g.Id == model.Id);

			if (siparis == null)
			{
				throw new Exception("Böyle bir sipariş mevcut değil!");
			}
			siparis.MenuBoyutu = model.MenuBoyutu;
			siparis.SiparisAdeti = model.SiparisAdeti;
			siparis.ToplamTutar = model.ToplamTutar;


			siparis.DataStatus = DataStatus.Updated;
			siparis.ModifiedDate = DateTime.Now;

			await _siparisRepository.Update(siparis);
		}
	}
}
