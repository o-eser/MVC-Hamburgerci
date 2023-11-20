using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Application.Services.Abstract;
using Hamburgerci.Entities.Concrete;
using Hamburgerci.Entities.Enum;
using Hamburgerci.Repositories.Abstract;

namespace Hamburgerci.Application.Services.Concrete
{
    public class EkstraMalzemeService : IEkstraMalzemeService
	{
		private readonly IEkstraMalzemeRepository _ekstraMalzemeRepository;

		public EkstraMalzemeService(IEkstraMalzemeRepository ekstraMalzemeRepository)
		{
			_ekstraMalzemeRepository = ekstraMalzemeRepository;
		}

		public async Task Create(EkstraMalzemeDTO model)
		{
			EkstraMalzeme ekstraMalzeme = new EkstraMalzeme
			{
				Adi = model.Adi,
				Fiyati = model.Fiyati,
				ParaBirimi = model.ParaBirimi
			};


			await _ekstraMalzemeRepository.Create(ekstraMalzeme);
		}

		public async Task Delete(int id)
		{
			EkstraMalzeme ekmalzeme = await _ekstraMalzemeRepository.GetDefault(g => g.Id == id);

			if (id == 0)
			{
				throw new ArgumentException("Id 0 Olamaz!");

			}
			else if (ekmalzeme == null)
			{
				throw new ArgumentException("Böyle bir ekstra malzeme mevcut değil!");
			}

			ekmalzeme.DataStatus = DataStatus.Deleted;
			ekmalzeme.DeletedDate = DateTime.Now;

			await _ekstraMalzemeRepository.Delete(ekmalzeme);
		}

		public async Task<EkstraMalzemeDTO> GetById(int id)
		{
			return await _ekstraMalzemeRepository.GetFilteredFirstOrDefault(x => new EkstraMalzemeDTO
			{
				Adi = x.Adi,
                Fiyati = x.Fiyati,
				ParaBirimi = x.ParaBirimi,
				Id = x.Id,
			}, g => g.Id == id && g.DataStatus != DataStatus.Deleted);
		}

		public async Task Update(EkstraMalzemeDTO model)
		{
			EkstraMalzeme ekstraMalzeme = new EkstraMalzeme();

			ekstraMalzeme = await _ekstraMalzemeRepository.GetDefault(g => g.Id == model.Id);
			if (ekstraMalzeme == null)
			{
				throw new Exception("Böyle bir ekstra malzeme mevcut değil!");
			}

			ekstraMalzeme.Adi = model.Adi;
			ekstraMalzeme.Fiyati = model.Fiyati;
			ekstraMalzeme.ParaBirimi = model.ParaBirimi;
			ekstraMalzeme.ModifiedDate = DateTime.Now;
			ekstraMalzeme.DataStatus = DataStatus.Updated;

			await _ekstraMalzemeRepository.Update(ekstraMalzeme);

		}

		public async Task<List<EkstraMalzemeDTO>?> GetAll()
		{
			return await _ekstraMalzemeRepository.GetFilteredList(x => new EkstraMalzemeDTO
			{
				Adi = x.Adi,
                Fiyati = x.Fiyati,
				ParaBirimi = x.ParaBirimi,
				Id = x.Id,
			}, g => g.DataStatus != DataStatus.Deleted) as List<EkstraMalzemeDTO>;
		}

        public async Task<List<EkstraMalzemeDTO>> Search(string searchText)
        {
            return await _ekstraMalzemeRepository.GetFilteredList(x => new EkstraMalzemeDTO
            {
				Adi = x.Adi,
				Fiyati=x.Fiyati,
                ParaBirimi = x.ParaBirimi,
                Id = x.Id 
                
            }, g => g.Adi.Contains(searchText) && g.DataStatus != DataStatus.Deleted) as List<EkstraMalzemeDTO>;
        }
    }
}
