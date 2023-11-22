using Hamburgerci.Domain.Entities.Concrete;
using Hamburgerci.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hamburgerci.Repositories.Context
{
	public class AppDbContext : IdentityDbContext<Kullanici, IdentityRole<int>, int>
	{
		public DbSet<Menu> Menuler { get; set; }
		public DbSet<EkstraMalzeme> EkstraMalzemeler { get; set; }
		public DbSet<Siparis> Siparisler { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			// Menü + MenüSipariş
			builder.Entity<Menu>().HasMany(m => m.MenuSiparisler).WithOne(ms => ms.Menu).HasForeignKey(ms => ms.MenuId);
			// Sipariş + MenüSipariş
			builder.Entity<Siparis>().HasMany(s => s.MenuSiparisler).WithOne(ms => ms.Siparis).HasForeignKey(s => s.SiparisId);

			builder.Entity<MenuSiparis>().HasKey(ms => new { ms.MenuId, ms.SiparisId });

			// EkstraMalzeme + EkstraMalzemeSipariş
			builder.Entity<EkstraMalzeme>().HasMany(m => m.EkstraMalzemeSiparisler).WithOne(ms => ms.EkstraMalzeme).HasForeignKey(ms => ms.EkstraMalzemeId);
			
			// Sipariş + EkstraMalzemeSipariş
			builder.Entity<Siparis>().HasMany(s => s.EkstraMalzemeSiparisler).WithOne(ms => ms.Siparis).HasForeignKey(s => s.SiparisId);

			builder.Entity<EkstraMalzemeSiparis>().HasKey(ms => new { ms.EkstraMalzemeId, ms.SiparisId });

			builder.Entity<Siparis>().HasOne(a => a.Kullanici).WithMany(a => a.Siparisler);
			base.OnModelCreating(builder);
		}
	}

}
