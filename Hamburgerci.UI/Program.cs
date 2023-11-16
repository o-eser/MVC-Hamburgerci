using Hamburgerci.Entities.Concrete;
using Hamburgerci.Repositories.Abstract;
using Hamburgerci.Repositories.Concrete;
using Hamburgerci.Repositories.Context;
using Hamburgerci.Repositories.Data;
using Hamburgerci.Services.Abstract;
using Hamburgerci.Services.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")),ServiceLifetime.Scoped);

builder.Services.AddScoped<ISiparisRepository, SiparisRepository>(); //Diyoruz ki ISiparisRepository g�rd���n yerde SiparisRepository kullan.
builder.Services.AddScoped<ISiparisService, SiparisManager>();
builder.Services.AddScoped<IEkstraMalzemeRepository, EkstraMalzemeRepository>();
builder.Services.AddScoped<IEkstraMalzemeService, EkstraMalzemeManager>();
builder.Services.AddScoped<IMenuService, MenuManager>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();

builder.Services.AddIdentity<Kullanici, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 3;
}
).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();





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

app.UseAuthorization();
app.UseAuthentication();
//SeedData.Seed(app);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
