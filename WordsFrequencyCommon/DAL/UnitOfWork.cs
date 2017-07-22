using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WordsFrequency.Common.DAL.Entities;
using WordsFrequency.Common.Extensions;

namespace WordsFrequency.Common.DAL
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        /// <summary>
        /// Indicates if this instance is disposed
        /// </summary>
        private bool _disposed;

        private DbSet<WordsCountBase> Words { get; set; }
        private DbSet<SourceTextBase> SourceText { get; set; }

        public UnitOfWork() : base("Name=WFData")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SWRuneMasterDbContext, SWRuneMaster.Migrations.Configuration>("SWRuneMaster"));
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> All<T>() where T : class
        {
            return Set<T>();
        }

        /// <summary>
        /// Finds the specified keys.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        public T Find<T>(params object[] keys) where T : class
        {
            return Set<T>().Find(keys);
        }

        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> where) where T : class
        {
            return Set<T>().Where(where);
        }

        public T Single<T>(Expression<Func<T, bool>> where) where T : class
        {
            return Set<T>().Single(where);
        }

        public T First<T>(Expression<Func<T, bool>> where) where T : class
        {
            return Set<T>().First(where);
        }

        /// <summary>
        /// Adds the specified entry.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        public T Add<T>(T entry) where T : class
        {
            return Set<T>().Add(entry);
        }

        /// <summary>
        /// Removes the specified entry.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        public T Remove<T>(T entry) where T : class
        {
            return Set<T>().Remove(entry);
        }

        public IEnumerable<T> Remove<T>(IEnumerable<T> entries) where T : class
        {
            Guard.Against<ArgumentNullException>(entries == null, "UnitOfWork.Remove: entries is null");
            return Set<T>().RemoveRange(entries);
        }

        public void RemoveAll<T>() where T : class
        {
            foreach (var p in Set<T>())
            {
                Entry(p).State = EntityState.Deleted;
            }
        }

        /// <summary>
        /// Attaches the specified entry.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        public T Attach<T>(T entry) where T : class
        {   
            var res = Set<T>().Attach(entry);
            Entry(entry).State = EntityState.Modified;
            return res;
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public new int SaveChanges()
        {
            return base.SaveChanges();
        }

        public int Count<T>() where T : class
        {
            return Set<T>().Count();
        }

        public int Count<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Set<T>().Count(predicate);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="IntegraleKlantrapportageContext" /> class.
        /// </summary>
        ~UnitOfWork()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the context. The underlying <see cref="T:System.Data.Objects.ObjectContext" /> is also disposed if it was created
        /// is by this context or ownership was passed to this context when this context was created.
        /// The connection to the database (<see cref="T:System.Data.Common.DbConnection" /> object) is also disposed if it was created
        /// is by this context or ownership was passed to this context when this context was created.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                base.Dispose(disposing);

                _disposed = true;
            }
        }
    }
}