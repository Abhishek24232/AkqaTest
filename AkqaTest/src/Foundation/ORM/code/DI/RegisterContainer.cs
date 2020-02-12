using AkqaTest.Foundation.ORM.Repositories;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
using Glass.Mapper.Sc.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace AkqaTest.Foundation.ORM.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISitecoreService>(_ => new SitecoreService(Sitecore.Context.Database));
            serviceCollection.AddScoped<IRequestContext>(x => new RequestContext(x.GetService<ISitecoreService>()));
            serviceCollection.AddScoped<IMvcContext>(x => new MvcContext(x.GetService<ISitecoreService>()));
            serviceCollection.AddScoped<IGlassHtml, GlassHtml>();
            serviceCollection.AddTransient<IRenderingRepository, RenderingRepository>();
        }
    }
}