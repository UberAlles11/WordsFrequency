using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WordsFrequency.Common.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> All<T>() where T : class;
        //IQueryable<T> AsQueryable<T>() where T : class;

        /// <summary>
        /// Finds the specified keys.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        T Find<T>(params object[] keys) where T : class;

        IEnumerable<T> Find<T>(Expression<Func<T, bool>> where) where T : class;

        T Single<T>(Expression<Func<T, bool>> where) where T : class;

        T First<T>(Expression<Func<T, bool>> where) where T : class;

        int Count<T>() where T : class;
        int Count<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Adds the specified entry.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        T Add<T>(T entry) where T : class;

        /// <summary>
        /// Removes the specified entry.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        T Remove<T>(T entry) where T : class;

        IEnumerable<T> Remove<T>(IEnumerable<T> entries) where T : class;

        void RemoveAll<T>() where T : class;

        /// <summary>
        /// Attaches the specified entry.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        T Attach<T>(T entry) where T : class;

        /// <summary>
        /// Saves the changes.
        /// </summary>
        int SaveChanges();
    }
}
