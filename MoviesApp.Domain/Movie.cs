using System;

namespace MoviesApp.Domain
{
    public class Movie : Entity<int>
    {
        public double Popularity { get; set; }
        public int VoteCount { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
