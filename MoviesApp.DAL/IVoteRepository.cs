using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.Domain;

namespace MoviesApp.DAL
{
    public interface IVoteRepository : ICrudRepository<Vote, (int, int)>
    {
    }
}
