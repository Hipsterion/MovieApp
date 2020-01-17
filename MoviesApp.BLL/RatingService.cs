using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Options;
using MoviesApp.Domain;
namespace MoviesApp.BLL
{
    public class RatingService
    {
        MovieService _movieService;
        MemberService _memberService;
        VoteService _voteService;
        public AppConfigData ConfigData { get; }

        public RatingService(IOptions<AppConfigData> configData, MovieService movieService, MemberService memberService, VoteService voteService)
        {
            _movieService = movieService;
            _memberService = memberService;
            _voteService = voteService;
        }

        public int GetMovieVoteCount(Movie movie)
        {
            return _voteService.GetVotes()
                .Where(v => v.Id.MovieId == movie.Id)
                .Count();
        }

        public int GetMovieRank(Movie movie)
        {
            return _movieService.GetMovies()
                        .OrderByDescending(m => GetMovieRating(m))
                        .Select((m, i) => new { Movie = m, Index = i })
                        .Where(x => x.Movie.Id == movie.Id)
                        .Select(x => x.Index)
                        .First() + 1;
        }

        public double GetMovieRating(Movie movie)
        {
            return GetMovieVoteCount(movie) > 0 ? _voteService.GetVotes()
                                                    .Where(v => v.Id.MovieId == movie.Id)
                                                    .Average(m => m.Score) : 0;
        }

        public IEnumerable<MovieRatingDTO> GetMoviesRatings()
        {
            return _movieService.GetMovies()
                        .Select(m => new MovieRatingDTO()
                        {
                            Id = m.Id,
                            Title = m.Title,
                            Rank = GetMovieRank(m),
                            VoteCount = GetMovieVoteCount(m),
                            Rating = GetMovieRating(m)
                        })
                        .ToList();
        }
    }
}
