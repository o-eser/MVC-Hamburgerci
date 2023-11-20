using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Application.Models.VMs;
using Hamburgerci.Application.Services.Abstract;
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

        public SiparisService(ISiparisRepository siparisRepository, IMenuRepository menuRepository, IEkstraMalzemeRepository ekstraMalzemeRepository)
        {
            _siparisRepository = siparisRepository;
            _menuRepository = menuRepository;
            _ekstraMalzemeRepository = ekstraMalzemeRepository;
        }


        public async Task Create(CreateSiparisDTO model)
        {
            Siparis siparis = new Siparis
            {
                MenuBoyutu = model.MenuBoyutu,
                SiparisAdeti = model.SiparisAdeti,
                ToplamTutar = model.ToplamTutar,
                Menuler = model.Menuler.Select(x => new Menu
                {
                    Id = x.Id,
                    MenuAdi = x.MenuAdi,
                    MenuFiyati = x.MenuFiyati,
                    ParaBirimi = x.ParaBirimi
                }).ToList(),
                EkstraMalzemeler = model.EkstraMalzemeler.Select(x => new EkstraMalzeme
                {
                    Id = x.Id,
                    Adi = x.Adi,
                    Fiyati = x.Fiyati,
                    ParaBirimi = x.ParaBirimi
                }).ToList(),
                CreatedDate = DateTime.Now,
                DataStatus = DataStatus.Inserted
            };

            await _siparisRepository.Create(siparis);
        }

        public async Task<CreateSiparisDTO> CreatePost()
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
                   x => x.DataStatus != DataStatus.Deleted) as ICollection<EkstraMalzemeVM>,

                Menuler = await _menuRepository.GetFilteredList(x => new MenuVM
                {
                    Id = x.Id,
                    MenuAdi = x.MenuAdi,
                    MenuFiyati = x.MenuFiyati,
                    ParaBirimi = x.ParaBirimi
                }, x => x.DataStatus != DataStatus.Deleted) as ICollection<MenuVM>
            };

            return createSiparis;
        }

        public async Task Delete(string id)
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
                EkstraMalzemeler = x.EkstraMalzemeler.Select(x => new EkstraMalzemeVM
                {
                    Id = x.Id,
                    Adi = x.Adi,
                    Fiyati = x.Fiyati,
                    ParaBirimi = x.ParaBirimi
                }).ToList(),
                Menuler = x.Menuler.Select(x => new MenuVM
                {
                    Id = x.Id,
                    MenuAdi = x.MenuAdi,
                    MenuFiyati = x.MenuFiyati,
                    ParaBirimi = x.ParaBirimi
                }).ToList(),
                MenuBoyutu = x.MenuBoyutu,
                SiparisAdeti = x.SiparisAdeti,
                ToplamTutar = x.ToplamTutar
            }, x => x.DataStatus != DataStatus.Deleted);

            return siparisler.ToList();
        }

        public async Task<List<UpdateSiparisDTO>?> Search(string searchText)
        {
            return await _siparisRepository.GetFilteredList(x => new UpdateSiparisDTO
            {
                EkstraMalzemeler = x.EkstraMalzemeler.Select(x => new EkstraMalzemeVM
                {
                    Id = x.Id,
                    Adi = x.Adi,
                    Fiyati = x.Fiyati,
                    ParaBirimi = x.ParaBirimi
                }).ToList(),
                Menuler = x.Menuler.Select(x => new MenuVM
                {
                    Id = x.Id,
                    MenuAdi = x.MenuAdi,
                    MenuFiyati = x.MenuFiyati,
                    ParaBirimi = x.ParaBirimi
                }).ToList(),
                MenuBoyutu = x.MenuBoyutu,
                SiparisAdeti = x.SiparisAdeti,
                ToplamTutar = x.ToplamTutar
            }, x => x.DataStatus != DataStatus.Deleted && x.Id.ToLower().Contains(searchText.ToLower())) as List<UpdateSiparisDTO>;
        }

        public async Task<UpdateSiparisDTO> GetById(string id)
        {
            return await _siparisRepository.GetFilteredFirstOrDefault(x => new UpdateSiparisDTO
            {
                EkstraMalzemeler = x.EkstraMalzemeler.Select(x => new EkstraMalzemeVM
                {
                    Id = x.Id,
                    Adi = x.Adi,
                    Fiyati = x.Fiyati,
                    ParaBirimi = x.ParaBirimi
                }).ToList(),
                Menuler = x.Menuler.Select(x => new MenuVM
                {
                    Id = x.Id,
                    MenuAdi = x.MenuAdi,
                    MenuFiyati = x.MenuFiyati,
                    ParaBirimi = x.ParaBirimi
                }).ToList(),
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
            siparis.Menuler = model.Menuler.Select(x => new Menu
            {
                Id = x.Id,
                MenuAdi = x.MenuAdi,
                MenuFiyati = x.MenuFiyati,
                ParaBirimi = x.ParaBirimi
            }).ToList();
            siparis.EkstraMalzemeler = model.EkstraMalzemeler.Select(x => new EkstraMalzeme
            {
                Id = x.Id,
                Adi = x.Adi,
                Fiyati = x.Fiyati,
                ParaBirimi = x.ParaBirimi
            }).ToList();
            siparis.DataStatus = DataStatus.Updated;
            siparis.ModifiedDate = DateTime.Now;

            await _siparisRepository.Update(siparis);
        }
    }
}
