namespace Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// A unified contract for the application data access layer.
    /// </summary>
    /// <typeparam name="T">The type parameter for an application entity &quot;model&quot; which has a Db table.</typeparam>
    /// <typeparam name="object">The type parameter for the entity's primary key.</typeparam>
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Returns a single object with a primary key of the provided id
        /// </summary>
        /// <param name="id">The primary key of the object to fetch</param>
        /// <returns>A single object with the provided primary key or null</returns>
        T Get(int id);

        /// <summary>
        /// An overloaded Get() to retrieve a specific portion of the relation.
        /// </summary>
        /// <param name="id">The primary key of the object to fetch.</param>
        /// <param name="numOfRecords">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>An ICollection of objects in range specified according to the arguments.</returns>
        ICollection<T> Get(int pageNumber, int numOfRecords, Expression<Func<T, int>> orderBy);

        /// <summary>
        /// Returns a single object with a primary key of the provided id
        /// </summary>
        /// <param name="id">The primary key of the object to fetch</param>
        /// <returns>A single object with the provided primary key or null</returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// An overloaded GetAsync() to retrieve a specific portion of the relation.
        /// </summary>
        /// <param name="id">The primary key of the object to fetch</param>
        /// <param name="numOfRecords">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>An ICollection of objects in range specified according to the arguments.</returns>
        Task<ICollection<T>> GetAsync(int pageNumber, int numOfRecords, Expression<Func<T, int>> orderBy);

        /// <summary>
        /// Gets a collection of all objects in the database
        /// </summary>
        /// <returns>An ICollection of every object in the database</returns>
        ICollection<T> GetAll();

        /// <summary>
        /// Gets a collection of all objects in the database
        /// </summary>
        /// <returns>An ICollection of every object in the database</returns>
        Task<ICollection<T>> GetAllAsync();

        /// <summary>
        /// Returns a single object which matches the provided expression
        /// </summary>
        /// <param name="predicate">A Linq expression filter to find a single result</param>
        /// <returns>A single object which matches the expression filter.
        /// If more than one object is found or if zero are found, null is returned</returns>
        T Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns a single object which matches the provided expression
        /// </summary>
        /// <param name="predicate">A Linq expression filter to find a single result</param>
        /// <returns>A single object which matches the expression filter.
        /// If more than one object is found or if zero are found, null is returned</returns>
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns a collection of objects which match the provided expression
        /// </summary>
        /// <param name="predicate">A linq expression filter to find one or more results</param>
        /// <returns>An ICollection of object which match the expression filter</returns>
        ICollection<T> FindAll(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns a collection of objects which match the provided expression
        /// </summary>
        /// <param name="predicate">A linq expression filter to find one or more results</param>
        /// <returns>An ICollection of object which match the expression filter</returns>
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Inserts a single object to the database and commits the change
        /// </summary>
        /// <param name="e">The object to insert</param>
        /// <returns>The resulting object including its primary key after the insert</returns>
        T Add(T e);

        /// <summary>
        /// Inserts a single object to the database and commits the change
        /// </summary>
        /// <param name="e">The object to insert</param>
        /// <returns>The resulting object including its primary key after the insert</returns>
        Task<T> AddAsync(T e);

        /// <summary>
        /// Inserts a collection of objects into the database and commits the changes
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="eList">An IEnumerable list of objects to insert</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys</returns>
        IEnumerable<T> AddAll(IEnumerable<T> eList);

        /// <summary>
        /// Inserts a collection of objects into the database and commits the changes
        /// </summary>
        /// <param name="eList">An IEnumerable list of objects to insert</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys</returns>
        Task<IEnumerable<T>> AddAllAsync(IEnumerable<T> eList);

        /// <summary>
        /// Updates a single object based on the provided primary key and commits the change
        /// </summary>
        /// <param name="updated">The updated object to apply to the database</param>
        /// <param name="key">The primary key of the object to update</param>
        /// <returns>The resulting updated object</returns>
        T Update(T updated, int key);

        /// <summary>
        /// Updates a single object based on the provided primary key and commits the change
        /// </summary>
        /// <param name="updated">The updated object to apply to the database</param>
        /// <param name="key">The primary key of the object to update</param>
        /// <returns>The resulting updated object</returns>
        Task<T> UpdateAsync(T updated, int key);

        /// <summary>
        /// Deletes a single object from the database and commits the change
        /// </summary>
        /// <param name="e">The object to delete</param>
        int Delete(T e);

        /// <summary>
        /// Deletes a single object from the database and commits the change
        /// </summary>
        /// <param name="e">The object to delete</param>
        Task<int> DeleteAsync(T e);

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