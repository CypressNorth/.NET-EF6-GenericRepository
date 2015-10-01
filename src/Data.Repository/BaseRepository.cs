namespace Data.Repository
{
    using Data.Repository.Context;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// A generic class for a basic Db relation operations.
    /// </summary>
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        #region Properties

        protected AppDbContext Db { get; set; }

        protected bool disposed { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// The constructor requires an open DataContext to work with.
        /// </summary>
        /// <param name="context">An optional open DataContext.</param>
        public BaseRepository(AppDbContext context = null)
        {
            Db = context ?? new AppDbContext();

            disposed = false;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns a single object with a primary key of the provided id.
        /// </summary>
        /// <param name="id">The primary key of the object to fetch.</param>
        /// <returns>A single object with the provided primary key or null</returns>
        public virtual T Get(int id)
        {
            return Db.Set<T>().Find(id);
        }

        /// <summary>
        /// An overloaded Get() to retrieve a specific portion of the relation.
        /// </summary>
        /// <param name="id">The primary key of the object to fetch.</param>
        /// <param name="numOfRecords">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>An ICollection of objects in range specified according to the arguments.</returns>
        public virtual ICollection<T> Get(int pageNumber, int numOfRecords, Expression<Func<T, int>> orderBy)
        {
            int recordstoPass = (pageNumber - 1) * numOfRecords;

            return Db.Set<T>().OrderBy(orderBy).Skip(recordstoPass).Take(numOfRecords).ToList();
        }

        /// <summary>
        /// Returns a single object with a primary key of the provided id.
        /// </summary>
        /// <param name="id">The primary key of the object to fetch.</param>
        /// <returns>A single object with the provided primary key or null.</returns>
        public virtual async Task<T> GetAsync(int id)
        {
            return await Db.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// An overloaded GetAsync() to retrieve a specific portion of the relation.
        /// </summary>
        /// <param name="id">The primary key of the object to fetch</param>
        /// <param name="numOfRecords">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>An ICollection of objects in range specified according to the arguments.</returns>
        public virtual async Task<ICollection<T>> GetAsync(int pageNumber, int numOfRecords, Expression<Func<T, int>> orderBy)
        {
            int recordstoPass = (pageNumber - 1) * numOfRecords;

            return await Db.Set<T>().OrderBy(orderBy).Skip(recordstoPass).Take(numOfRecords).ToListAsync();
        }

        /// <summary>
        /// Gets a collection of all objects in the database.
        /// </summary>
        /// <returns>An ICollection of every object in the database.</returns>
        public virtual ICollection<T> GetAll()
        {
            return Db.Set<T>().ToList();
        }

        /// <summary>
        /// Gets a collection of all objects in the database.
        /// </summary>
        /// <returns>An ICollection of every object in the database.</returns>
        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await Db.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Returns a single object which matches the provided expression.
        /// </summary>
        /// <param name="filter">A Linq expression filter to find a single result.</param>
        /// <returns>A single object which matches the expression filter.
        /// If more than one object is found or if zero are found, null is returned.</returns>
        public virtual T Find(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = Db.Set<T>();

            return Db.Set<T>().SingleOrDefault(filter);
        }

        /// <summary>
        /// Returns a single object which matches the provided expression.
        /// </summary>
        /// <param name="filter">A Linq expression filter to find a single result.</param>
        /// <returns>A single object which matches the expression filter.
        /// If more than one object is found or if zero are found, null is returned.</returns>
        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> filter)
        {
            return await Db.Set<T>().SingleOrDefaultAsync(filter);
        }

        /// <summary>
        /// Returns a collection of objects which match the provided expression.
        /// </summary>
        /// <param name="filter">A linq expression filter to find one or more results.</param>
        /// <returns>An ICollection of object which match the expression filter.</returns>
        public virtual ICollection<T> FindAll(Expression<Func<T, bool>> filter)
        {
            return Db.Set<T>().Where(filter).ToList();
        }

        /// <summary>
        /// Returns a collection of objects which match the provided expression.
        /// </summary>
        /// <param name="filter">A linq expression filter to find one or more results.</param>
        /// <returns>An ICollection of object which match the expression filter.</returns>
        public virtual async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> filter)
        {
            return await Db.Set<T>().Where(filter).ToListAsync();
        }

        /// <summary>
        /// Inserts a single object to the database and commits the change.
        /// </summary>
        /// <param name="e">The object to insert.</param>
        /// <returns>The resulting object including its primary key after the insert.</returns>
        public virtual T Add(T e)
        {
            Db.Set<T>().Add(e);
            Db.SaveChanges();
            return e;
        }

        /// <summary>
        /// Inserts a single object to the database and commits the change.
        /// </summary>
        /// <param name="e">The object to insert.</param>
        /// <returns>The resulting object including its primary key after the insert.</returns>
        public virtual async Task<T> AddAsync(T e)
        {
            Db.Set<T>().Add(e);
            await Db.SaveChangesAsync();
            return e;
        }

        /// <summary>
        /// Inserts a collection of objects into the database and commits the changes.
        /// </summary>
        /// <param name="eList">An IEnumerable list of objects to insert.</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys.</returns>
        public virtual IEnumerable<T> AddAll(IEnumerable<T> eList)
        {
            Db.Set<T>().AddRange(eList);
            Db.SaveChanges();
            return eList;
        }

        /// <summary>
        /// Inserts a collection of objects into the database and commits the changes.
        /// </summary>
        /// <param name="eList">An IEnumerable list of objects to insert.</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys.</returns>
        public virtual async Task<IEnumerable<T>> AddAllAsync(IEnumerable<T> eList)
        {
            Db.Set<T>().AddRange(eList);
            await Db.SaveChangesAsync();
            return eList;
        }

        /// <summary>
        /// Updates a single object based on the provided primary key and commits the change.
        /// </summary>
        /// <param name="updated">The updated object to apply to the database.</param>
        /// <param name="key">The primary key of the object to update.</param>
        /// <returns>The resulting updated object.</returns>
        public virtual T Update(T updated, int key)
        {
            if (updated == null)
                return null;

            T existing = Db.Set<T>().Find(key);
            if (existing != null)
            {
                Db.Entry(existing).CurrentValues.SetValues(updated);
                Db.SaveChanges();
            }
            return existing;
        }

        /// <summary>
        /// Updates a single object based on the provided primary key and commits the change.
        /// </summary>
        /// <param name="updated">The updated object to apply to the database.</param>
        /// <param name="key">The primary key of the object to update.</param>
        /// <returns>The resulting updated object.</returns>
        public virtual async Task<T> UpdateAsync(T updated, int key)
        {
            if (updated == null)
                return null;

            T existing = await Db.Set<T>().FindAsync(key);
            if (existing != null)
            {
                Db.Entry(existing).CurrentValues.SetValues(updated);
                await Db.SaveChangesAsync();
            }
            return existing;
        }

        /// <summary>
        /// Deletes a single object from the database and commits the change.
        /// </summary>
        /// <param name="e">The object to delete.</param>
        /// <returns>The number of effected objects.</returns>
        public virtual int Delete(T e)
        {
            Db.Set<T>().Remove(e);
            return Db.SaveChanges();
        }

        /// <summary>
        /// Deletes a single object from the database and commits the change.
        /// </summary>
        /// <param name="e">The object to delete.</param>
        /// <returns>The number of effected objects.</returns>
        public virtual async Task<int> DeleteAsync(T e)
        {
            Db.Set<T>().Remove(e);
            return await Db.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the count of the number of objects in the database.
        /// </summary>
        /// <returns>The count of the number of objects.</returns>
        public virtual int Count()
        {
            return Db.Set<T>().Count();
        }

        /// <summary>
        /// Gets the count of the number of objects in the database.
        /// </summary>
        /// <returns>The count of the number of objects.</returns>
        public virtual async Task<int> CountAsync()
        {
            return await Db.Set<T>().CountAsync();
        }

        #endregion Methods

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Db.Dispose();
            }

            disposed = true;
        }

        #endregion IDisposable Implementation
    }
}