using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoviesApp.Domain
{
    public class Vote : Entity<(int MemberId, int MovieId)>
    {
        [Required]
        [Range(1,10)]
        public int Score { get; set; }

        public VoteDTO ToVoteDTO()
        {
            return new VoteDTO()
            {
                MemberId = Id.MemberId,
                MovieId = Id.MovieId,
                Score = Score
            };
        }
    }
}
