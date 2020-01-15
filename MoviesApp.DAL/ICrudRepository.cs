using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.Domain;
namespace MoviesApp.DAL
{
    public interface ICrudRepository<T, Type> where T : Entity<Type>
    {
        public IEnumerable<T> GetAll();
        public T GetById(Type id);
        public T Insert(T obj);
        public T Update(T obj);
        public void Delete(Type id);
        public void Save();
    }
}
