using System.Collections.Generic;
using WordsFrequency.Common.DAL.Entities;

namespace WordsFrequency.Common.DAL
{
    public interface IDataRepository<T> where T : EntityBase
    {
        void Add(T entity);
        void Delete(IEnumerable<T> entities);
        void DeleteAll();
        void Delete(T entity);
        void Delete(long id);
        T GetById(long id);
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Save();
        int Count();
    }
}