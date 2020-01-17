using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Domain;
using MoviesApp.BLL;
using Microsoft.Extensions.Logging;
namespace MoviesApp.API.Controllers
{
    [Route("movies/ratings")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private RatingService _service;
        private readonly ILogger<RatingController> _logger;

        public RatingController(ILogger<RatingController> logger, RatingService service)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet]
        public IEnumerable<MovieRatingDTO> Get()
        {
            return _service.GetMoviesRatings();
        }

       
    }
}
