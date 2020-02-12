
using AkqaTest.Feature.Media.Models;
using Glass.Mapper.Sc.Web.Mvc;
using AkqaTest.Foundation.DependencyInjection.Attributes;
using AkqaTest.Foundation.ORM.Repositories;
using AkqaTest.Foundation.Logging.Repositories;
using AkqaTest.Foundation.Logging.Exceptions;
using System;
using System.Linq;

namespace AkqaTest.Feature.Media.Repositories
{
    [Lifetime(Lifetime.Singleton,typeof(IMediaRepository))]
    public class MediaRepository : IMediaRepository
    {
        private readonly IMvcContext _context;
        private readonly IRenderingRepository _renderingRepository;
        private readonly ILogRepository _logRepository;

        public MediaRepository(IMvcContext context, ILogRepository logRepository, IRenderingRepository renderingRepository)
        {
            _context = context;
            _logRepository = logRepository;
            _renderingRepository = renderingRepository;
        }
        public ICarsouselContainer GetConatinerDatasource()
        {
            var dataSource = _renderingRepository.GetDataSourceItem<ICarsouselContainer>();

            if (dataSource == null || !dataSource.TemplateId.Equals(new Guid(Constants.Carousel.CarouselContainerTempalteID)))
            {
                    _logRepository.Warn(Logging.Error.DataSourceError);
                    throw new InvalidDataSourceItemException($"Item should be not null and derived from {Constants.Carousel.CarouselContainerTempalteID} template");
            }

            return dataSource;

        }

        public IImageCarousel GetCarouselDatasource()
        {
            var dataSource = _renderingRepository.GetDataSourceItem<IImageCarousel>();

            if (dataSource == null || !dataSource.TemplateId.Equals(new Guid(Constants.Carousel.CarouselItemTempalteID)))
            {
                _logRepository.Warn(Logging.Error.DataSourceError);
                throw new InvalidDataSourceItemException($"Item should be not null and derived from {Constants.Carousel.CarouselItemTempalteID} template");
            }            

            return dataSource;
        }

    }
}