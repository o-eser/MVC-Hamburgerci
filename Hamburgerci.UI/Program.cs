using Hamburgerci.Application.Services.Abstract;
using Hamburgerci.Application.Services.Concrete;
using Hamburgerci.Domain.Repositories;
using Hamburgerci.Entities.Concrete;
using Hamburgerci.Infrastructure.Repositories;
using Hamburgerci.Repositories.Abstract;
using Hamburgerci.Repositories.Concrete;
using Hamburgerci.Repositories.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")),ServiceLifetime.Scoped);

builder.Services.AddScoped<ISiparisRepository, SiparisRepository>(); //Diyoruz ki ISiparisRepository gördüðün yerde SiparisRepository kullan.
builder.Services.AddScoped<ISiparisService, SiparisService>();
builder.Services.AddScoped<IEkstraMalzemeRepository, EkstraMalzemeRepository>();
builder.Services.AddScoped<IEkstraMalzemeService, EkstraMalzemeService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMenuSiparisRepository, MenuSiparisRepository>();
builder.Services.AddScoped<IEkstraMalzemeSiparisRepository, EkstraMalzemeSiparisRepository>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IAppUserService, AppUserService>();

builder.Services.AddIdentity<AppUser, IdentityRole<int>>(opt =>
{
    opt.SignIn.RequireConfirmedEmail = false;
    opt.SignIn.RequireConfirmedPhoneNumber = false;
    opt.SignIn.RequireConfirmedAccount = false;
    opt.User.RequireUniqueEmail = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequireLowercase = false;
}
).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

//builder.Services.AddDistributedMemoryCache();

//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(5);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.UseSession();

//SeedData.Seed(app);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
