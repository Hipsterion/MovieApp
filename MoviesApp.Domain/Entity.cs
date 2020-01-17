using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoviesApp.Domain
{
    public class Entity<T>
    {
        [Required]
        public T Id { get; set; }
    }
}
