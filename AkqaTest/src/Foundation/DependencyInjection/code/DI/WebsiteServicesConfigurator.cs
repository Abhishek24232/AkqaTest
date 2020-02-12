
using Microsoft.Extensions.DependencyInjection;
using AkqaTest.Foundation.DependencyInjection.Extensions;
using Sitecore.DependencyInjection;

namespace AkqaTest.Foundation.DI.DependencyInjection
{
    public class WebsiteServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {            
            serviceCollection.AddMvcControllers("*.Feature.*");
            serviceCollection.AddServiceAttribute("*.Feature.*");
            serviceCollection.AddServiceAttribute("*.Foundation.*");
        }
    }
}