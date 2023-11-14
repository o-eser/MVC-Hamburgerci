using Hamburgerci.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburgerci.Repositories.Context
{
    public class AppDbContext : IdentityDbContext<Kullanici,IdentityRole<int>,int>
    {
        public DbSet<Menu> Menuler { get; set; }
        public DbSet<EkstraMalzeme> EkstraMalzemeler { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

    }
}
