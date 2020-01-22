using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.Domain;
namespace MoviesApp.DAL
{
    public interface IMovieRepository : ICrudRepository<Movie, int>
    {
        public int GetMovieVotesCount(int id);
        public double GetMovieRating(int id);
        public IEnumerable<Vote> GetMovieVotes(int id);
    }
}
