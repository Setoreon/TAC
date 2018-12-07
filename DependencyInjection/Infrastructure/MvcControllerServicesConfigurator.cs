using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace DependencyInjection.Infrastructure
{
    public class MvcControllerServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvcControllers("accounts.tac.local");
        }
        
    }
}