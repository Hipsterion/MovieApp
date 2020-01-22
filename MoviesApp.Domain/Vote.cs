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

        public int MemberId { get { return Id.MemberId; } set { Id = (value, Id.MovieId); } }
        public int MovieId { get { return Id.MovieId; } set { Id = (Id.MemberId, value); } }

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
