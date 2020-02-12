using AkqaTest.Feature.Media.Models;

namespace AkqaTest.Feature.Media.Repositories
{
    public interface IMediaRepository
    {
        ICarsouselContainer GetConatinerDatasource();
        IImageCarousel GetCarouselDatasource();
    }
}