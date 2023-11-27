using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Hamburgerci.Application.Services.Abstract;
using Hamburgerci.Application.Services.Concrete;
using Hamburgerci.Repositories.Abstract;
using Hamburgerci.Repositories.Concrete;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Hamburgerci.Application.IoC
{
	public class DependencyResolver : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
			builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerDependency();

			builder.RegisterType<SiparisService>().As<ISiparisService>().InstancePerLifetimeScope();
			builder.RegisterType<SiparisRepository>().As<ISiparisRepository>().InstancePerDependency();

			builder.RegisterType<EkstraMalzemeService>().As<IEkstraMalzemeService>().InstancePerLifetimeScope();
			builder.RegisterType<EkstraMalzemeRepository>().As<IEkstraMalzemeRepository>().InstancePerDependency();

			builder.RegisterType<MenuService>().As<IMenuService>().InstancePerLifetimeScope();
			builder.RegisterType<MenuRepository>().As<IMenuRepository>().InstancePerDependency();



			builder.RegisterType<EmailSender>().As<IEmailSender>();


			base.Load(builder);
		}
	}
}
