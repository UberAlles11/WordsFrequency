using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WordsFrequency.Common.DAL.Entities;

namespace WordsFrequency.Common.DAL
{
    public class DbDataRepository<T> : IDataRepository<T>, IDisposable
    where T : EntityBase
    {
        internal DbContext context;
        internal DbSet<T> dbSet;

        public DbDataRepository()
        {
            context = new DataContext();
            this.dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            Guard.Against<ArgumentNullException>(entity == null, "DbDataRepository.Add: entity is null");
            dbSet.Add(entity);
        }
        public void Delete(IEnumerable<T> entities)
        {
            Guard.Against<ArgumentNullException>(entities == null, "DbDataRepository.Delete: entities is null");
            dbSet.RemoveRange(entities);
        }
        public void Delete(T entity)
        {
            Guard.Against<ArgumentNullException>(entity == null, "DbDataRepository.Delete: entity is null");
            dbSet.Remove(entity);
        }

        public void Delete(long id)
        {
            dbSet.Remove(dbSet.Find(id));
        }

        public void DeleteAll()
        {
            foreach (var p in dbSet)
            {
                context.Entry(p).State = EntityState.Deleted;
            }
            context.SaveChanges();
        }

        public T GetById(long id)
        {
            return dbSet.Find(id);
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.AsEnumerable();
        }
        public IQueryable<T> GetAllQueryable()
        {
            return dbSet.AsQueryable();
        }
        public void Update(T entity)
        {
            Guard.Against<ArgumentNullException>(entity == null, "DbDataRepository.Update: entity is null");
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }

        public int Count()
        {
            return dbSet.Count();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
