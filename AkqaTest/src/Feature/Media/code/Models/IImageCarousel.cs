
using Glass.Mapper.Sc.Fields;
using Glass.Mapper.Sc.Configuration.Attributes;
using AkqaTest.Foundation.ORM.Models;

namespace AkqaTest.Feature.Media.Models
{
    [SitecoreType(TemplateId = Constants.Carousel.CarouselItemTempalteID, AutoMap = true)]
    public interface IImageCarousel : IGlassBase
    {
        string CtaText { get; set; }
        Link CtaLink { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        Image ImageMobile { get; set; }
        Image ImageDesktop { get; set; }

    }
}