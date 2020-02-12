namespace AkqaTest.Foundation.SitecoreExtensions.Extensions
{
  using System.Web;
  using AkqaTest.Foundation.SitecoreExtensions.Repositories;
  using AkqaTest.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Mvc.Helpers;

  public static class SitecoreExtensions
  {
    public static string Dictionary(this SitecoreHelper helper, string relativePath, string defaultValue = "")
    {
      return DictionaryPhraseRepository.Current.Get(relativePath, defaultValue);
    }

    public static HtmlString DictionaryField(this SitecoreHelper helper, string relativePath, string defaultValue = "")
    {
      var item = DictionaryPhraseRepository.Current.GetItem(relativePath, defaultValue);
      if (item == null)
        return new HtmlString(defaultValue);
      return helper.Field(Constants.DictionaryEntry.Fields.Phrase, item);
    }
  }
}