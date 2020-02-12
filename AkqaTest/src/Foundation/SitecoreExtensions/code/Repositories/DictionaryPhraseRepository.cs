namespace AkqaTest.Foundation.SitecoreExtensions.Repositories
{
    using System;
    using System.Linq;
    using System.Web;
    using Sitecore;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using AkqaTest.Foundation.SitecoreExtensions.Models;
    using AkqaTest.Foundation.SitecoreExtensions;

    public class DictionaryPhraseRepository : IDictionaryPhraseRepository
    {
        public Dictionary Dictionary { get; set; }

        public DictionaryPhraseRepository(Dictionary dictionary)
        {
            this.Dictionary = dictionary;
        }

        public static IDictionaryPhraseRepository Current => GetCurrentFromCache();

        private static IDictionaryPhraseRepository GetCurrentFromCache()
        {
            if (HttpContext.Current != null)
            {
                var repository = HttpContext.Current.Items["DictionaryPhraseRepository.Current"] as IDictionaryPhraseRepository;
                if (repository != null)
                {
                    return repository;
                }
            }
            var returnValue = new DictionaryPhraseRepository(DictionaryRepository.Current);

            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items.Add("DictionaryPhraseRepository.Current", returnValue);
            }
            return returnValue;
        }

        public string Get(string relativePath, string defaultValue)
        {
            if (relativePath == null)
            {
                throw new ArgumentNullException(nameof(relativePath));
            }
            if (Context.Database == null)
            {
                return defaultValue;
            }

            var dictionaryItem = this.GetDictionaryItem(relativePath, defaultValue);
            if (dictionaryItem == null)
            {
                return defaultValue;
            }

            return dictionaryItem.Fields[AkqaTest.Foundation.SitecoreExtensions.Constants.DictionaryEntry.Fields.Phrase].Value ?? defaultValue;
        }

        public Item GetItem(string relativePath, string defaultValue = "")
        {
            if (relativePath == null)
            {
                throw new ArgumentNullException(nameof(relativePath));
            }

            var item = this.GetDictionaryItem(relativePath, defaultValue);
            if (item == null)
            {
                Log.Debug($"Could not find the dictionary item for the site '{this.Dictionary.Site.Name}' with the path '{relativePath}'", this);
            }
            return item;
        }

        private Item GetDictionaryItem(string relativePath, string defaultValue)
        {
            try
            {
                relativePath = AssertRelativePath(relativePath);

                var item = this.Dictionary.Root.Axes.GetItem(relativePath);
                if (item != null)
                    return item;

                if (defaultValue == null)
                    return null;
            }

            catch (Exception ex)
            {
                Log.Error($"Failed to get {relativePath} from the dictionary in site {this.Dictionary.Site.Name}", ex, this);
                return null;
            }
            return null;
        }

        private static string AssertRelativePath(string relativePath)
        {
            if (relativePath == null)
            {
                throw new ArgumentNullException(nameof(relativePath));
            }

            if (relativePath.StartsWith("/"))
            {
                relativePath = relativePath.Substring(1);
            }
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                throw new ArgumentException("the path is not a valid relative path", nameof(relativePath));
            }
            return relativePath;
        }
    }
}