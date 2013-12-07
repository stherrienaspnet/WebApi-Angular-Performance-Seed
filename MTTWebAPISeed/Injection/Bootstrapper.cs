using System.Net.Http;
using System.Web.Http;
using MTTWebAPI.Domain.Injection;
using MTTWebAPI.WebUI.Security;
using Microsoft.Practices.Unity;

namespace MTTWebAPI.WebUI.Injection
{
    public class Bootstrapper
    {
	    private static IUnityContainer _container;
		public static IUnityContainer Container { get { return _container; } }

        public static void Initialise()
        {
			_container = BuildUnityContainer();
			GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(_container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
			DomainInjectionRegistration.RegisterPackage(container);
	        RegisterWebPackage(container);

            return container;
        }

	    private static void RegisterWebPackage(UnityContainer container)
	    {
		    container.RegisterType<IWebApiConfig, WebApiConfig>();
			container.RegisterType<DelegatingHandler, TokenInspector>();
	    }
    }
}