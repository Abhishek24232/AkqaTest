using System;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using Sitecore.Globalization;

namespace AkqaTest.Foundation.ORM.Models
{
    public interface IGlassBase
    {
        [SitecoreId]
        Guid Id { get; set; }

        [SitecoreItem]
        Item Item { get; set; }

        [SitecoreInfo(SitecoreInfoType.TemplateId)]
        Guid TemplateId { get; set; }

        [SitecoreInfo(SitecoreInfoType.Language)]
        Language Language { get; set; }

        [SitecoreInfo(SitecoreInfoType.Version)]
        int Version { get; set; }

        [SitecoreInfo(SitecoreInfoType.Url, UrlOptions =
            SitecoreInfoUrlOptions.Default |
            SitecoreInfoUrlOptions.SiteResolving |
            SitecoreInfoUrlOptions.UseItemLanguage)]
        string Url { get; set; }

        [SitecoreInfo(SitecoreInfoType.Url, UrlOptions =
            SitecoreInfoUrlOptions.Default |
            SitecoreInfoUrlOptions.AlwaysIncludeServerUrl |
            SitecoreInfoUrlOptions.SiteResolving |
            SitecoreInfoUrlOptions.UseItemLanguage)]
        string AbsoluteUrl { get; set; }

        [SitecoreInfo(SitecoreInfoType.Path)]
        string Path { get; set; }

        [SitecoreInfo(SitecoreInfoType.DisplayName)]
        string DisplayName { get; set; }

        [SitecoreInfo(SitecoreInfoType.Name)]
        string Name { get; set; }

        [SitecoreParent]
        IGlassBase Parent { get; set; }
    }
}
