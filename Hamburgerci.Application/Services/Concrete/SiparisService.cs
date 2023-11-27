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

        private readonly IAppUserService _appUserService;

		public SiparisService(ISiparisRepository siparisRepository, IMenuRepository menuRepository, IEkstraMalzemeRepository ekstraMalzemeRepository, IMenuSiparisRepository menuSiparisRepository, IEkstraMalzemeSiparisRepository esRepository, IAppUserService appUserService)
		{
			_siparisRepository = siparisRepository;
			_menuRepository = menuRepository;
			_ekstraMalzemeRepository = ekstraMalzemeRepository;
			_msRepository = menuSiparisRepository;
			_esRepository = esRepository;
			_appUserService = appUserService;
		}


		public async Task Create(CreateSiparisDTO model)
        {
			#region toplam tutar hesaplama
			var selectedMenuler = model.Menuler?.Where(m => m.Adet > 0) ?? Enumerable.Empty<MenuVM>();
			var selectedEkstraMalzemeler = model.EkstraMalzemeler?.Where(e => e.Adet > 0) ?? Enumerable.Empty<EkstraMalzemeVM>();

			// menü ve ekstra malzeme toplam tutarı 
			double toplamTutar = selectedMenuler.Sum(m => m.Adet * m.Fiyati) +
								 selectedEkstraMalzemeler.Sum(e => e.Adet * e.Fiyati);
            switch (model.MenuBoyutu)
            {
                case MenuBoyutu.Kucuk: 
                    toplamTutar=toplamTutar * 1;
					break;
                case MenuBoyutu.Orta:
					toplamTutar = toplamTutar * 1.15;
					break;
                case MenuBoyutu.Buyuk:
					toplamTutar = toplamTutar * 1.25;
					break;
                default:
                    break;
            }
			#endregion



			Siparis siparis = new Siparis
            {
                KullaniciId =await _appUserService.GetUserId(),
                MenuBoyutu = model.MenuBoyutu,
                SiparisAdeti = model.SiparisAdeti,
                ToplamTutar= toplamTutar*model.SiparisAdeti,
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
                    Fiyati = x.Fiyati
                },
                   x => x.DataStatus != DataStatus.Deleted) as List<EkstraMalzemeVM>,

                Menuler = await _menuRepository.GetFilteredList(x => new MenuVM
                {
                    Id = x.Id,
                    MenuAdi = x.MenuAdi,
                    Fiyati = x.MenuFiyati
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
            //var menuler = await _menuRepository.GetFilteredList(x => new MenuVM
            //{
            //    Id = x.Id,
            //    MenuAdi = x.MenuAdi,
            //    MenuFiyati = x.MenuFiyati,
            //    ParaBirimi = x.ParaBirimi
            //}, x => x.DataStatus != DataStatus.Deleted) as List<MenuVM>;

            //var ekstraMalzemeler = await _ekstraMalzemeRepository.GetFilteredList(x => new EkstraMalzemeVM
            //{
            //    Id = x.Id,
            //    Adi = x.Adi,
            //    Fiyati = x.Fiyati,
            //    ParaBirimi = x.ParaBirimi
            //}, x => x.DataStatus != DataStatus.Deleted) as List<EkstraMalzemeVM>;

            var kullaniciId = await _appUserService.GetUserId();
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
                }).ToList()
            },
            x => x.DataStatus != DataStatus.Deleted &&x.KullaniciId== kullaniciId,
            include: x => x.Include(x => x.EkstraMalzemeSiparisler).
                                        ThenInclude(x => x.EkstraMalzeme)
            .Include(x => x.MenuSiparisler)
            .ThenInclude(x => x.Menu)) as List<UpdateSiparisDTO>;



            foreach (var siparis in siparisler)
            {
                List<MenuVM> m = new List<MenuVM>();

                m = await _menuRepository.GetFilteredList(x => new MenuVM
                {
                    Id = x.Id,
                    MenuAdi = x.MenuAdi,
                    Fiyati = x.MenuFiyati,
                }, x => x.DataStatus != DataStatus.Deleted) as List<MenuVM>;
                siparis.Menuler = m;


                List<EkstraMalzemeVM> e = new List<EkstraMalzemeVM>();
                e = await _ekstraMalzemeRepository.GetFilteredList(x => new EkstraMalzemeVM
                {
                    Id = x.Id,
                    Adi = x.Adi,
                    Fiyati = x.Fiyati
                }, x => x.DataStatus != DataStatus.Deleted) as List<EkstraMalzemeVM>; ;

                siparis.EkstraMalzemeler = e;
            }





            foreach (UpdateSiparisDTO siparis in siparisler)
            {
                foreach (var menu in siparis.Menuler)
                {
                    foreach (var ms in siparis.MenuSiparis)
                    {
                        if (menu.Id == ms.MenuId)
                        {
                            menu.Adet = ms.MenuAdeti;
                        }
                    }
                }

                foreach (var ekstra in siparis.EkstraMalzemeler)
                {
                    foreach (var es in siparis.EkstraMalzemeSiparis)
                    {
                        if (ekstra.Id == es.EkstraMalzemeId)
                        {
                            ekstra.Adet = es.EkstraMalzemeAdeti;
                        }
                    }
                }
            }



            return siparisler;
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

            List<MenuSiparis> menuSiparisler = new List<MenuSiparis>();

            foreach (var item in model.Menuler)
            {
                if (item.Adet != 0)
                {
                    MenuSiparis ms = new MenuSiparis
                    {
                        MenuId = item.Id,
                        SiparisId = siparis.Id,
                        Adet = item.Adet
                    };

                    if (siparis.MenuSiparisler.Any(x => x.MenuId == item.Id))
                    {
                        ms.DataStatus = DataStatus.Updated;
                        ms.ModifiedDate = DateTime.Now;

                        //await _msRepository.Update(ms);
                    }
                    else
                    {
                        ms.DataStatus = DataStatus.Inserted;
                        ms.CreatedDate = DateTime.Now;

                        await _msRepository.Create(ms);
                    }
                    menuSiparisler.Add(ms);
                }
            }


            List<EkstraMalzemeSiparis> ekstraSiparisler = new List<EkstraMalzemeSiparis>();

            foreach (var item in model.EkstraMalzemeler)
            {
                if (item.Adet != 0)
                {
                    EkstraMalzemeSiparis em = new EkstraMalzemeSiparis
                    {
                        EkstraMalzemeId = item.Id,
                        SiparisId = siparis.Id,
                        Adet = item.Adet,
                    };

                    if (siparis.EkstraMalzemeSiparisler.Any(x => x.EkstraMalzemeId == item.Id))
                    {
                        em.DataStatus = DataStatus.Updated;
                        em.ModifiedDate = DateTime.Now;

                        //await _esRepository.Update(em);
                    }
                    else
                    {
                        em.DataStatus = DataStatus.Inserted;
                        em.CreatedDate = DateTime.Now;

                        await _esRepository.Create(em);
                    }
                    ekstraSiparisler.Add(em);
                }
            }


            entity.MenuBoyutu = model.MenuBoyutu;
            entity.SiparisAdeti = model.SiparisAdeti;
            entity.ToplamTutar = model.ToplamTutar;

            entity.MenuSiparisler = menuSiparisler;
            entity.EkstraMalzemeSiparisler = ekstraSiparisler;

            entity.DataStatus = DataStatus.Updated;
            entity.ModifiedDate = DateTime.Now;

            await _siparisRepository.Update(entity);
        }
    }
}
