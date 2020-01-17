using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoviesApp.Domain
{
    public class VoteDTO
    {
        [Required]
        public int MemberId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        [Range(1,10)]
        public int Score { get; set; }

        public Vote ToVote()
        {
            return new Vote()
            {
                Id = (MemberId, MovieId),
                Score = Score
            };
        }
    }
}
