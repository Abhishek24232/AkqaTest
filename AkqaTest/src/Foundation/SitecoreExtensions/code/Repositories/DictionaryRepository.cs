namespace AkqaTest.Foundation.SitecoreExtensions.Repositories
{
  using System;
  using System.Configuration;
  using Sitecore.Data.Items;
  using AkqaTest.Foundation.SitecoreExtensions.Models;
  using Sitecore.Sites;

  public class DictionaryRepository : IDictionaryRepository
  {
    private const string MasterDatabaseName = "master";

    public static Dictionary Current => new DictionaryRepository().Get(SiteContext.Current);

    public Dictionary Get(SiteContext site)
    {
      return new Dictionary()
             {
               Site = site,
               Root = this.GetDictionaryRoot(site),
             };
    }

    private Item GetDictionaryRoot(SiteContext site)
    {
      var dictionaryPath = site.Properties["dictionaryPath"];
      if (dictionaryPath == null)
        throw new ConfigurationErrorsException("No dictionaryPath was specified on the <site> definition.");
      var rootItem = site.Database.GetItem(dictionaryPath);
      if (rootItem == null)
        throw new ConfigurationErrorsException("The root item specified in the dictionaryPath on the <site> definition was not found.");
      return rootItem;
    }

  }
}