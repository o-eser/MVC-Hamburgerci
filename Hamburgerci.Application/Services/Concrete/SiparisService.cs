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
using Microsoft.EntityFrameworkCore;

namespace Hamburgerci.Application.Services.Concrete
{
	public class SiparisService : ISiparisService
	{
		private readonly ISiparisRepository _siparisRepository;
		private readonly IMenuRepository _menuRepository;
		private readonly IEkstraMalzemeRepository _ekstraMalzemeRepository;

		private readonly IMenuSiparisRepository _msRepository;
		private readonly IEkstraMalzemeSiparisRepository _esRepository;

		public SiparisService(ISiparisRepository siparisRepository, IMenuRepository menuRepository, IEkstraMalzemeRepository ekstraMalzemeRepository, IMenuSiparisRepository menuSiparisRepository, IEkstraMalzemeSiparisRepository esRepository)
		{
			_siparisRepository = siparisRepository;
			_menuRepository = menuRepository;
			_ekstraMalzemeRepository = ekstraMalzemeRepository;
			_msRepository = menuSiparisRepository;
			_esRepository = esRepository;
		}


		public async Task Create(CreateSiparisDTO model)
		{
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

			foreach (var item in model.Menuler)
			{
				if (item.Adet != 0)
				{
					MenuSiparis ms = new MenuSiparis
					{
						MenuId = item.Id,
						SiparisId = siparis.Id,
						Adet = item.Adet,
						DataStatus = DataStatus.Inserted,
						CreatedDate = DateTime.Now
					};
					await _msRepository.Create(ms);

					menuSiparisler.Add(ms);
				}
			}

			siparis.MenuSiparisler = menuSiparisler;

			List<EkstraMalzemeSiparis> ekstraMalzemeSiparisler = new List<EkstraMalzemeSiparis>();
			foreach (var item in model.EkstraMalzemeler)
			{
				if (item.Adet != 0)
				{

					EkstraMalzemeSiparis em = new EkstraMalzemeSiparis
					{
						EkstraMalzemeId = item.Id,
						SiparisId = siparis.Id,
						Adet = item.Adet,
						DataStatus = DataStatus.Inserted,
						CreatedDate = DateTime.Now
					};

					await _esRepository.Create(em);

					ekstraMalzemeSiparisler.Add(em);

				}
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
			Siparis siparis = await _siparisRepository.GetFilteredFirstOrDefault(x => new Siparis
			{
				Id = x.Id,
				MenuBoyutu = x.MenuBoyutu,
				SiparisAdeti = x.SiparisAdeti,
				//ToplamTutar=x.ToplamTutar
				//}, x => new Siparis { Id = x.Id, MenuBoyutu = x.MenuBoyutu, SiparisAdeti = x.SiparisAdeti, ToplamTutar = x.ToplamTutar}
				EkstraMalzemeSiparisler = x.EkstraMalzemeSiparisler,
				MenuSiparisler = x.MenuSiparisler,
			},
			x => x.Id == id,
			include: x => x.Include(x => x.EkstraMalzemeSiparisler)
										.Include(x => x.MenuSiparisler));


			// Silmek istediklerimizi id den yakalayıp ef core ın tanıması için
			Siparis entity = await _siparisRepository.GetDefault(x => x.Id == id);


			if (id == null)
			{
				throw new ArgumentException("Id 0 Olamaz!");

			}
			else if (entity == null)
			{
				throw new ArgumentException("Böyle bir sipariş mevcut değil!");
			}

			foreach (var item in siparis.MenuSiparisler)
			{
				item.DataStatus = DataStatus.Deleted;
				item.DeletedDate = DateTime.Now;
			}

			foreach (var item in siparis.EkstraMalzemeSiparisler)
			{
				item.DataStatus = DataStatus.Deleted;
				item.DeletedDate = DateTime.Now;
			}

			entity.DataStatus = DataStatus.Deleted;
			entity.DeletedDate = DateTime.Now;


			await _siparisRepository.Delete(entity);

		}

		public async Task<List<UpdateSiparisDTO>?> GetAll()
		{
			var menuler=await _menuRepository.GetFilteredList(x=>new MenuVM
			{
				Id=x.Id,
				MenuAdi=x.MenuAdi,
				MenuFiyati=x.MenuFiyati,
				ParaBirimi=x.ParaBirimi
			},x=>x.DataStatus!=DataStatus.Deleted) as List<MenuVM>;

			var ekstraMalzemeler=await _ekstraMalzemeRepository.GetFilteredList(x=>new EkstraMalzemeVM
			{
				Id=x.Id,
				Adi=x.Adi,
				Fiyati=x.Fiyati,
				ParaBirimi=x.ParaBirimi
			},x=>x.DataStatus!=DataStatus.Deleted) as List<EkstraMalzemeVM>;

			
			var siparisler = await _siparisRepository.GetFilteredList(x => new UpdateSiparisDTO
			{
				Id = x.Id,
				MenuBoyutu = x.MenuBoyutu,
				SiparisAdeti = x.SiparisAdeti,
				ToplamTutar = x.ToplamTutar,
				EkstraMalzemeSiparis = x.EkstraMalzemeSiparisler.Select(x => new EkstraMalzemeSiparisDTO
				{
					EkstraMalzemeId = x.EkstraMalzemeId,
					EkstraMalzemeAdeti = x.Adet,
					EkstraMazlemeAdi = x.EkstraMalzeme.Adi
				}).ToList(),
				MenuSiparis = x.MenuSiparisler.Select(x => new MenuSiparisDTO
				{
					MenuId = x.MenuId,
					MenuAdeti = x.Adet,
					MenuAdi = x.Menu.MenuAdi
				}).ToList(),
				
			},
			x => x.DataStatus != DataStatus.Deleted,
			include: x => x.Include(x => x.EkstraMalzemeSiparisler).
										ThenInclude(x => x.EkstraMalzeme)
			.Include(x => x.MenuSiparisler)
			.ThenInclude(x => x.Menu));


			

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
			Siparis entity = new Siparis();

			entity = await _siparisRepository.GetDefault(g => g.Id == model.Id);


			Siparis siparis = await _siparisRepository.GetFilteredFirstOrDefault(x => new Siparis
			{
				Id = x.Id,
				MenuBoyutu = x.MenuBoyutu,
				SiparisAdeti = x.SiparisAdeti,
				//ToplamTutar=x.ToplamTutar
				//}, x => new Siparis { Id = x.Id, MenuBoyutu = x.MenuBoyutu, SiparisAdeti = x.SiparisAdeti, ToplamTutar = x.ToplamTutar}
				EkstraMalzemeSiparisler = x.EkstraMalzemeSiparisler,
				MenuSiparisler = x.MenuSiparisler,
			},
			x => x.Id == entity.Id,
			include: x => x.Include(x => x.EkstraMalzemeSiparisler)
										.Include(x => x.MenuSiparisler));



			if (entity == null)
			{
				throw new Exception("Böyle bir sipariş mevcut değil!");
			}

			if (model.MenuSiparis != null)
				entity.MenuSiparisler = model.MenuSiparis.Select(x => new MenuSiparis
				{
					MenuId = x.MenuId,
					Adet = x.MenuAdeti,
					DataStatus = DataStatus.Updated,
					ModifiedDate = DateTime.Now,
					SiparisId = siparis.Id
				}).ToList();

			if (model.EkstraMalzemeSiparis != null)
				entity.EkstraMalzemeSiparisler = model.EkstraMalzemeSiparis.Select(x => new EkstraMalzemeSiparis
				{
					EkstraMalzemeId = x.EkstraMalzemeId,
					Adet = x.EkstraMalzemeAdeti,
					DataStatus = DataStatus.Updated,
					ModifiedDate = DateTime.Now,
					SiparisId = siparis.Id
				}).ToList();


			entity.MenuBoyutu = model.MenuBoyutu;
			entity.SiparisAdeti = model.SiparisAdeti;
			entity.ToplamTutar = model.ToplamTutar;


			entity.DataStatus = DataStatus.Updated;
			entity.ModifiedDate = DateTime.Now;

			await _siparisRepository.Update(entity);
		}
	}
}
