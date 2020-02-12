
using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections.Generic;

namespace AkqaTest.Feature.Media.Models
{
    public interface ICarasouselBase
    {
        [SitecoreChildren(InferType =true)]
        IEnumerable<IImageCarousel> Children { get; set; }
    }
}