using Hamburgerci.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburgerci.Repositoriess.Context
{
    public class AppDbContext : DbContext 
    {
        public DbSet<Menu> Menuler { get; set; }
        public DbSet<EkstraMalzeme> EkstraMalzemeler { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

    }
}
