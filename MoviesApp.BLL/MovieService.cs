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
        private IMovieRepository _repository;
        public AppConfigData ConfigData { get; }

        public MovieService(IOptions<AppConfigData> configData, IMovieRepository repository)
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

        public Movie UpdateMovie(Movie movie)
        {
            return _repository.Update(movie);
        }

        public void DeleteMovie(int id)
        {
            _repository.Delete(id);
        }

        public int GetMovieVotesCount(int id)
        {
            return _repository.GetMovieVotesCount(id);
        }

        public double GetMovieRating(int id)
        {
            return _repository.GetMovieRating(id);
        }

        public IEnumerable<Vote> GetMovieVotes(int id)
        {
            return _repository.GetMovieVotes(id);
        }


    }
}
