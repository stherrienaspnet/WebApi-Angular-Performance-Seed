using MTTWebAPI.Domain.Repositories.Abstract;
using MTTWebAPI.Domain.Repositories.Concrete;
using MTTWebAPI.Domain.Services.Abstract;
using MTTWebAPI.Domain.Services.Concrete;
using Microsoft.Practices.Unity;

namespace MTTWebAPI.Domain.Injection
{
	public class DomainInjectionRegistration
	{
		public static void RegisterPackage(IUnityContainer container)
		{
			// register all your components with the container here
			container.RegisterType<IMembershipService, MembershipService>();
			container.RegisterType<IWebSecurityService, WebSecurityService>();
			container.RegisterType<IFeatureRepository, FeatureRepository>();
		}
	}
}
