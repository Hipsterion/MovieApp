using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MoviesApp.Domain;
using MoviesApp.DAL;
namespace MoviesApp.BLL
{
    public class MovieService
    {
        private ICrudRepository<Movie, int> _repository;
        public AppConfigData ConfigData { get; }

        public MovieService(IOptions<AppConfigData> configData, ICrudRepository<Movie, int> repository)
        {
            ConfigData = configData.Value;
            _repository = repository;
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _repository.GetAll();
        }

        public Movie GetMovie(int id)
        {
            return _repository.GetById(id);
        }

        public Movie AddMovie(Movie movie)
        {
            return _repository.Insert(movie);
        }

        /*public List<Movie> GetPopularMovies(int number)
        {
            return _movieList.OrderByDescending(m => m.Popularity).Take(number).ToList();
        }*/

        /*public List<Movie> GetRandomMovies()
        {
            var rng = new Random();
            return Enumerable.Range(1, 10).Select(index => new Movie
            {
                ReleaseDate = DateTime.Now.AddDays(index),
                VoteCount = rng.Next(-20, 55),
                Title = Summaries[rng.Next(Summaries.Length)],
                Popularity = 1000 + index
            })
            .ToList();
        }*/

     /*   public void Add(Movie movie)
        {
            _movieList.Add(movie);
        }

        public void Delete(int id)
        {
            _movieList = _movieList.Where(m => m.Id != id).ToList();
        }

        public void Update(Movie movie)
        {
            var oldMovie = _movieList.FirstOrDefault(m => m.Id == movie.Id);
            oldMovie.Popularity = movie.Popularity;
            oldMovie.ReleaseDate = movie.ReleaseDate;
            oldMovie.Title = movie.Title;
            oldMovie.VoteCount = movie.VoteCount;
        }*/
    }
}
