using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AkqaTest.Feature.Media.Repositories;

namespace AkqaTest.Feature.Media.Controllers
{
    public class MediaFeatureController : Controller
    {
        private readonly IMediaRepository _mediaRepository;

        public MediaFeatureController(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }
        public ActionResult CarouselContainer()
        {
            var model = _mediaRepository.GetConatinerDatasource();
            return View(model);
        }

        public ActionResult Carousel()
        {
            var model = _mediaRepository.GetCarouselDatasource();
            return View(model);
        }
    }
}