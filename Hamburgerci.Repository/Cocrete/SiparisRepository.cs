using Hamburgerci.Entities.Concrete;
using Hamburgerci.Repository.Abstract;
using Hamburgerci.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hamburgerci.Repository.Cocrete
{
    public class SiparisRepository : ISiparisRepository
    {
        private readonly AppDbContext _context;

        public SiparisRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool AddSiparis(Siparis siparis)
        {
            _context.Siparisler.Add(siparis);
            return Save() > 0;
        }

        public bool DeleteSiparis(int id)
        {
            _context.Siparisler.Remove(GetSiparisById(id));
            return Save() > 0;
        }

        public List<Siparis> GetAllSiparisler()
        {
            return _context.Siparisler.ToList();
        }

        public Siparis GetSiparisById(int id)
        {
            return _context.Siparisler.Find(id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public bool UpdateSiparis(Siparis siparis)
        {
            _context.Siparisler.Update(siparis);
            return Save() > 0;
        }
    }
}
