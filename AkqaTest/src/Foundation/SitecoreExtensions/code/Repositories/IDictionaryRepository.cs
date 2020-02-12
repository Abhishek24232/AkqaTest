using AkqaTest.Foundation.SitecoreExtensions.Models;
using Sitecore.Sites;

namespace AkqaTest.Foundation.SitecoreExtensions.Repositories
{
  public interface IDictionaryRepository
  {
    Dictionary Get(SiteContext site);
  }
}