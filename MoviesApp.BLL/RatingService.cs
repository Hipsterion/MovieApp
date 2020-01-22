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

        public IEnumerable<MovieRatingDTO> GetMoviesRatings()
        {
            return _movieService.GetMovies()
                        .Select(m => new
                        {
                            MovieId = m.Id,
                            m.Title,
                            VoteCount = _movieService.GetMovieVotesCount(m.Id),
                            Rating = _movieService.GetMovieRating(m.Id)
                        })
                        .OrderByDescending(m => m.Rating)
                        .Select((m,i) => new MovieRatingDTO
                        {
                            MovieId = m.MovieId,
                            Title = m.Title,
                            VoteCount = m.VoteCount,
                            Rating = m.Rating,
                            Rank = i
                        })
                        .ToList();
        }
    }
}
