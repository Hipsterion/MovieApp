using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.Domain
{
    public class Entity<T>
    {
        public T Id { get; set; }
    }
}
