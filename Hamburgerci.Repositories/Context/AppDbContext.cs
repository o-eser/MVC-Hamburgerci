using Hamburgerci.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Siparis>()
                .HasMany(a=>a.EkstraMalzemeler)
                .WithMany(a=>a.Siparisler)
                .UsingEntity(j=>j.ToTable( "SiparisEsktraMalzemeler"));
            builder.Entity<Siparis>()
                .HasMany(a=>a.Menuler)
                .WithMany(a=>a.Siparisler)
                .UsingEntity(j => j.ToTable("SiparisMenuler"));
            builder.Entity<Siparis>().HasOne(a=>a.Kullanici).WithMany(a=>a.Siparisler);
            base.OnModelCreating(builder);
        }
    }
    
}
