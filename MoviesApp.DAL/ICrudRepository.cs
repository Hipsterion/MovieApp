using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.Domain;
namespace MoviesApp.DAL
{
    public interface ICrudRepository<T, TKey> where T : Entity<TKey>
    {
        public IEnumerable<T> GetAll();
        public T GetById(TKey id);
        public T Insert(T obj);
        public T Update(T obj);
        public void Delete(TKey id);
    }
}
