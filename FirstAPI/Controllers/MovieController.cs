using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesApp.BLL;
using MoviesApp.Domain;

namespace MoviesApp.API.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MovieController : ControllerBase
    {

        private MovieService _service;
        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger, MovieService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Movie> GetMovies()
        {
            return _service.GetMovies();
        }

        [HttpGet]
        [Route("{id}")]
        public Movie GetMovie(int id)
        {
            return _service.GetMovie(id);
        }

        [HttpPost]
        public ActionResult PostMovie(Movie movie)
        {
            _service.AddMovie(movie);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteMovie(int id)
        {
            _service.DeleteMovie(id);
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateMovie(Movie movie)
        {
            _service.UpdateMovie(movie);
            return Ok();
        }

        /*[HttpGet]
        [Route("popular/{number}")]

        public IEnumerable<Movie> GetPopularMovies(int number)
        {
            return _service.GetPopularMovies(number);
        }*/

    }
}
