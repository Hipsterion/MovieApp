using System;

namespace MoviesApp.Domain
{
    public class Movie
    {
        private static int _count = 1;
        public int Id { get; set; }
        public float Popularity { get; set; }
        public int VoteCount { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }

        public Movie()
        {
            Id = _count;
            _count++;
        }
    }
}
