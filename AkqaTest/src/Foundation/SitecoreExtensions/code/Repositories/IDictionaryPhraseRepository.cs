using Sitecore.Data.Items;

namespace AkqaTest.Foundation.SitecoreExtensions.Repositories
{
  public interface IDictionaryPhraseRepository
  {
    string Get(string relativePath, string defaultValue = "");
    Item GetItem(string relativePath, string defaultValue = "");
  }
}