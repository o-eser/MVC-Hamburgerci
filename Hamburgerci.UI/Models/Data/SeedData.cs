using Bogus;
using Hamburgerci.Entities.Concrete;
using Hamburgerci.Entities.Enum;
using Hamburgerci.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace Hamburgerci.Repositories.Data
{
    public class SeedData
    {
        #region Bogus Codes
        public static readonly Faker<Kullanici> kullaniciFaker = new Faker<Kullanici>("tr")
            .RuleFor(x => x.FirstName, f => f.Name.FullName());

        public static readonly Faker<Menu> menuFaker = new Faker<Menu>("tr")
            .RuleFor(x => x.MenuAdi, f => f.Commerce.ProductName())
            .RuleFor(x => x.MenuFiyati, f => f.Random.Int(200, 450))
            .RuleFor(x => x.ParaBirimi, f => f.PickRandom<ParaBirimi>());

        public static readonly Faker<EkstraMalzeme> ekstraFaker = new Faker<EkstraMalzeme>("tr")
            .RuleFor(x => x.EkstraMalzemeAdi, f => f.Commerce.ProductName())
            .RuleFor(x => x.EkstraMalzemeFiyati, f => f.Random.Int(10, 100))
            .RuleFor(x => x.ParaBirimi, f => f.PickRandom<ParaBirimi>());

        public static readonly Faker<Siparis> siparisFaker = new Faker<Siparis>("tr")
            .RuleFor(x => x.MenuBoyutu, f => f.PickRandom<MenuBoyutu>())
            .RuleFor(x => x.SiparisAdeti, f => f.Random.Int(1, 5))
            .RuleFor(x => x.Menuler, f => menuFaker.Generate(f.Random.Int(1, 3)))
            .RuleFor(x => x.EkstraMalzemeler, f => ekstraFaker.Generate(f.Random.Int(1, 3)));

        public static readonly List<Siparis> siparisler = siparisFaker.Generate(10);
        public static readonly List<Kullanici> kullanicilar = kullaniciFaker.Generate(3);
        #endregion

        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                AppDbContext context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.Migrate(); // uygulama çalıştığında veritabanı yoksa oluşturur, varsa günceller.
                if (!context.Users.Any()) // veritabanında kayıt varsa false gelecek
                {
                    context.Users.AddRange(kullanicilar);
                    context.Siparisler.AddRange(siparisler);
                }
            }
        }
    }
}

