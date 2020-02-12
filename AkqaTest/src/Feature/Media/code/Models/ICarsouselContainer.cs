

using Glass.Mapper.Sc.Fields;
using System.Collections.Generic;
using AkqaTest.Foundation.ORM.Models;

namespace AkqaTest.Feature.Media.Models
{
    public interface ICarsouselContainer : IGlassBase
    {
        string Title { get; set; }

        string SubTitle { get; set; }
        string Description { get; set; }
        ICarasouselBase Datasource { get; set; }
        IEnumerable<IImageCarousel> CarouselList { get; set; }
        string CtaText { get; set; }
        Link CtaLink { get; set; }
        int InitialItemsMobile { get; set; }
        int InitialItemsDesktop { get; set; }
    }
}