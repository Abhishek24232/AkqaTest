
using AkqaTest.Foundation.Logging.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace AkqaTest.Foundation.Logging.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {            
            serviceCollection.AddTransient<ILogRepository, LogRepository>();
        }
    }
}