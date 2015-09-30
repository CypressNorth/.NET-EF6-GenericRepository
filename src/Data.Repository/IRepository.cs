namespace Data.Repository
{
    using Data.Repository.Context;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// A unified contract for the application data access layer.
    /// </summary>
    /// <typeparam name="TEntity">The type parameter for an application entity &quot;model&quot; which has a Db table.</typeparam>
    /// <typeparam name="object">The type parameter for the entity's primary key.</typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// Returns a single object with a primary key of the provided id
        /// </summary>
        /// <param name="id">The primary key of the object to fetch</param>
        /// <returns>A single object with the provided primary key or null</returns>
        TEntity Get(object id);

        /// <summary>
        /// Returns a single object with a primary key of the provided id
        /// </summary>
        /// <param name="id">The primary key of the object to fetch</param>
        /// <returns>A single object with the provided primary key or null</returns>
        Task<TEntity> GetAsync(object id);

        /// <summary>
        /// Gets a collection of all objects in the database
        /// </summary>
        /// <returns>An ICollection of every object in the database</returns>
        ICollection<TEntity> GetAll();

        /// <summary>
        /// Gets a collection of all objects in the database
        /// </summary>
        /// <returns>An ICollection of every object in the database</returns>
        Task<ICollection<TEntity>> GetAllAsync();

        /// <summary>
        /// Returns a single object which matches the provided expression
        /// </summary>
        /// <param name="predicate">A Linq expression filter to find a single result</param>
        /// <returns>A single object which matches the expression filter.
        /// If more than one object is found or if zero are found, null is returned</returns>
        TEntity Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Returns a single object which matches the provided expression
        /// </summary>
        /// <param name="predicate">A Linq expression filter to find a single result</param>
        /// <returns>A single object which matches the expression filter.
        /// If more than one object is found or if zero are found, null is returned</returns>
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Returns a collection of objects which match the provided expression
        /// </summary>
        /// <param name="predicate">A linq expression filter to find one or more results</param>
        /// <returns>An ICollection of object which match the expression filter</returns>
        ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Returns a collection of objects which match the provided expression
        /// </summary>
        /// <param name="predicate">A linq expression filter to find one or more results</param>
        /// <returns>An ICollection of object which match the expression filter</returns>
        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Inserts a single object to the database and commits the change
        /// </summary>
        /// <param name="e">The object to insert</param>
        /// <returns>The resulting object including its primary key after the insert</returns>
        TEntity Add(TEntity e);

        /// <summary>
        /// Inserts a single object to the database and commits the change
        /// </summary>
        /// <param name="e">The object to insert</param>
        /// <returns>The resulting object including its primary key after the insert</returns>
        Task<TEntity> AddAsync(TEntity e);

        /// <summary>
        /// Inserts a collection of objects into the database and commits the changes
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="eList">An IEnumerable list of objects to insert</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys</returns>
        IEnumerable<TEntity> AddAll(IEnumerable<TEntity> eList);

        /// <summary>
        /// Inserts a collection of objects into the database and commits the changes
        /// </summary>
        /// <param name="eList">An IEnumerable list of objects to insert</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys</returns>
        Task<IEnumerable<TEntity>> AddAllAsync(IEnumerable<TEntity> eList);

        /// <summary>
        /// Updates a single object based on the provided primary key and commits the change
        /// </summary>
        /// <param name="updated">The updated object to apply to the database</param>
        /// <param name="key">The primary key of the object to update</param>
        /// <returns>The resulting updated object</returns>
        TEntity Update(TEntity updated, object key);

        /// <summary>
        /// Updates a single object based on the provided primary key and commits the change
        /// </summary>
        /// <param name="updated">The updated object to apply to the database</param>
        /// <param name="key">The primary key of the object to update</param>
        /// <returns>The resulting updated object</returns>
        Task<TEntity> UpdateAsync(TEntity updated, object key);

        /// <summary>
        /// Deletes a single object from the database and commits the change
        /// </summary>
        /// <param name="e">The object to delete</param>
        int Delete(TEntity e);

        /// <summary>
        /// Deletes a single object from the database and commits the change
        /// </summary>
        /// <param name="e">The object to delete</param>
        Task<int> DeleteAsync(TEntity e);

        /// <summary>
        /// Gets the count of the number of objects in the databse
        /// </summary>
        /// <returns>The count of the number of objects</returns>
        int Count();

        /// <summary>
        /// Gets the count of the number of objects in the databse
        /// </summary>
        /// <returns>The count of the number of objects</returns>
        Task<int> CountAsync();
    }
}