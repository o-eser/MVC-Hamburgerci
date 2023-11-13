using Hamburgerci.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburgerci.Repository.Abstract
{
    public interface ISiparisRepository
    {
        List<Siparis> GetAllSiparisler();

        Siparis GetSiparisById(int id);

        bool AddSiparis(Siparis siparis);

        bool UpdateSiparis(Siparis siparis);

        bool DeleteSiparis(int id);

        int Save();


    }
}
