using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Domain
{
    public class Movie : Entity<int>
    {
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
    }
}