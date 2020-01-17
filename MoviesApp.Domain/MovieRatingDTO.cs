using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.Domain
{
    public class MovieRatingDTO : Entity<int>
    {
        public string Title { get; set; }
        public int Rank { get; set; }
        public int VoteCount { get; set; }
        public double Rating { get; set; }
    }
}
