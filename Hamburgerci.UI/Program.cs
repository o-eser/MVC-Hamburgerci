using Autofac.Extensions.DependencyInjection;
using Autofac;
using Hamburgerci.Application.IoC;
using Hamburgerci.Application.Services.Abstract;
using Hamburgerci.Application.Services.Concrete;
using Hamburgerci.Domain.Repositories;
using Hamburgerci.Entities.Concrete;
using Hamburgerci.Infrastructure.Repositories;
using Hamburgerci.Repositories.Abstract;
using Hamburgerci.Repositories.Concrete;
using Hamburgerci.Repositories.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Hamburgerci.Repositories.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")),ServiceLifetime.Scoped);


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
	builder.RegisterModule(new DependencyResolver());
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddIdentity<AppUser, IdentityRole<int>>(opt =>
{
    opt.SignIn.RequireConfirmedEmail = true;
    opt.SignIn.RequireConfirmedPhoneNumber = false;
    opt.SignIn.RequireConfirmedAccount = false;
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequireLowercase = false;
}
).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/AppUser/Login";
    opt.AccessDeniedPath = "/AppUser/ErisimEngellendi";
});





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


SeedData.Seed(app);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Siparis}/{action=Index}/{id?}");

app.Run();
