
using System;
using Bogus;
using Hamburgerci.Entities.Concrete;
using Hamburgerci.Entities.Enum;
using Hamburgerci.Repositories.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hamburgerci.Repositories.Data
{
	public class SeedData
	{

		public async static Task Seed(IApplicationBuilder app)
		{

			using (var serviceScope = app.ApplicationServices.CreateScope())
			{
				AppDbContext context = serviceScope.ServiceProvider.GetService<AppDbContext>();
				UserManager<AppUser> _userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
				RoleManager<IdentityRole<int>> _roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole<int>>>();


				context.Database.Migrate(); // uygulama çalıştığında veritabanı yoksa oluşturur, varsa günceller.

				if (!context.Users.Any()) // veritabanında kayıt varsa false gelecek
				{
					//Role Oluşturma
					string[] roleNames = { "Admin", "User", "Editor" };

					IdentityResult roleResult;

					foreach (var roleName in roleNames)
					{
						var roleExist = await _roleManager.RoleExistsAsync(roleName);

						if (!roleExist)
						{
							// Rolü oluştur
							roleResult = await _roleManager.CreateAsync(new IdentityRole<int>(roleName));
						}
					}


					AppUser userAdmin = new AppUser()
					{
						Email = "admim@admin.com",
						UserName = "admin",
						PhoneNumber = "123456789",
						EmailConfirmed = true,
						PhoneNumberConfirmed = true,
						CreatedDate = System.DateTime.Now,
						DataStatus = DataStatus.Inserted,
					};

					AppUser user = new AppUser()
					{
						Email = "user@user.com",
						UserName = "user",
						PhoneNumber = "123456789",
						EmailConfirmed = true,
						PhoneNumberConfirmed = true,
						CreatedDate = System.DateTime.Now,
						DataStatus = DataStatus.Inserted,
					};

					AppUser user2 = new AppUser()
					{
						Email = "user2@user.com",
						UserName = "user2",
						PhoneNumber = "123456789",
						EmailConfirmed = true,
						PhoneNumberConfirmed = true,
						CreatedDate = System.DateTime.Now,
						DataStatus = DataStatus.Inserted,
					};

					var user3 = new AppUser()
					{
						Email = "user3@user.com",
						UserName = "user3",
						PhoneNumber = "123456789",
						EmailConfirmed = true,
						PhoneNumberConfirmed = true,
						CreatedDate = System.DateTime.Now,
						DataStatus = DataStatus.Inserted,
					};

					var user4 = new AppUser()
					{
						Email = "user4@user.com",
						UserName = "user4",
						PhoneNumber = "123456789",
						EmailConfirmed = true,
						PhoneNumberConfirmed = true,
						CreatedDate = System.DateTime.Now,
						DataStatus = DataStatus.Inserted,
					};

					IdentityResult identityResult = await _userManager.CreateAsync(userAdmin, "123");

					if (identityResult.Succeeded)
					{
						await _userManager.AddToRoleAsync(userAdmin, "Admin");
					}

					identityResult = await _userManager.CreateAsync(user, "123");

					if (identityResult.Succeeded)
					{
						await _userManager.AddToRoleAsync(user, "User");
					}

					identityResult= await _userManager.CreateAsync(user2, "123");
					if (identityResult.Succeeded)
					{
						await _userManager.AddToRoleAsync(user2, "User");
					}

					identityResult = await _userManager.CreateAsync(user3, "123");
					if (identityResult.Succeeded)
					{
						await _userManager.AddToRoleAsync(user3, "User");
					}

					identityResult=await _userManager.CreateAsync(user4, "123");
					if (identityResult.Succeeded)
					{
						await _userManager.AddToRoleAsync(user4, "User");
					}
				}

				if (!context.Menuler.Any())
				{
					List<Menu> menuList = new List<Menu>()
					{
						new Menu()
						{
							MenuAdi = "Big Mac",
							MenuFiyati = 250,
							CreatedDate = System.DateTime.Now,
							DataStatus = DataStatus.Inserted,
						},
						new Menu()
						{
							MenuAdi = "McChicken",
							MenuFiyati = 200,
							CreatedDate = System.DateTime.Now,
							DataStatus = DataStatus.Inserted,
						},
						new Menu()
						{
							MenuAdi = "McRoyal",
							MenuFiyati = 150,
							CreatedDate = System.DateTime.Now,
							DataStatus = DataStatus.Inserted,
						},
						new Menu()
						{
							MenuAdi = "McWrap",
							MenuFiyati = 100,
							CreatedDate = System.DateTime.Now,
							DataStatus = DataStatus.Inserted,
						},
						new Menu()
						{
							MenuAdi = "McFish",
							MenuFiyati = 100,
							CreatedDate = System.DateTime.Now,
							DataStatus = DataStatus.Inserted,
						},
						new Menu()
						{
							MenuAdi = "McToast",
							MenuFiyati = 50,
							CreatedDate = System.DateTime.Now,
							DataStatus = DataStatus.Inserted,
						},
					};

					await context.Menuler.AddRangeAsync(menuList);
					
				}

				if (!context.EkstraMalzemeler.Any())
				{
					List<EkstraMalzeme> ekstraMalzemeler = new List<EkstraMalzeme>{
						new EkstraMalzeme()
						{
							Adi = "Hardal",
							Fiyati = 5,
							CreatedDate = System.DateTime.Now,
							DataStatus = DataStatus.Inserted,
						},
						new EkstraMalzeme()
						{
							Adi = "Ketcap",
							Fiyati = 5,
							CreatedDate = System.DateTime.Now,
							DataStatus = DataStatus.Inserted,
						},
						new EkstraMalzeme()
						{
							Adi = "Mayonez",
							Fiyati = 5,
							CreatedDate = System.DateTime.Now,
							DataStatus = DataStatus.Inserted,
						},
						new EkstraMalzeme()
						{
							Adi = "Soğan",
							Fiyati = 5,
							CreatedDate = System.DateTime.Now,
							DataStatus = DataStatus.Inserted,
						},
						new EkstraMalzeme()
						{
							Adi = "Marul",
							Fiyati = 5,
							CreatedDate = System.DateTime.Now,
							DataStatus = DataStatus.Inserted,
						},
						new EkstraMalzeme()
						{
							Adi = "Domates",
							Fiyati = 5,
							CreatedDate = System.DateTime.Now,
							DataStatus = DataStatus.Inserted,
						},
					};

					await context.EkstraMalzemeler.AddRangeAsync(ekstraMalzemeler);
				}

				await context.SaveChangesAsync();


				// Siparişleri oluştur
				//List<Siparis> siparisler = new Faker<Siparis>()
				//	.RuleFor(s => s.MenuBoyutu, f => f.PickRandom<MenuBoyutu>())
				//	.RuleFor(s => s.SiparisAdeti, f => f.Random.Number(1, 5))
				//	.RuleFor(s => s.ToplamTutar, (f, s) => CalculateTotalTutar(s.SiparisAdeti, f.PickRandom(menus), f.PickRandom(ekstraMalzemeler)))
				//	.RuleFor(s => s.KullaniciId, f => f.IndexFaker)
				//	.Generate(10);
			}
		}

		static double? CalculateTotalTutar(int siparisAdeti, Menu menu, EkstraMalzeme ekstraMalzeme)
		{
			// Burada toplam tutar hesaplamalarını gerçekleştirin
			// Örnek bir hesaplama: (Menü fiyatı + Ekstra malzeme fiyatı) * Sipariş Adeti
			return (menu.MenuFiyati + ekstraMalzeme.Fiyati) * siparisAdeti;
		}

	}
}

