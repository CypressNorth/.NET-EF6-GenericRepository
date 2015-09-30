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
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
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
        public virtual TEntity Get(object id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Returns a single object with a primary key of the provided id.
        /// </summary>
        /// <param name="id">The primary key of the object to fetch.</param>
        /// <returns>A single object with the provided primary key or null.</returns>
        public virtual async Task<TEntity> GetAsync(object id)
        {
            return await Db.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Gets a collection of all objects in the database.
        /// </summary>
        /// <returns>An ICollection of every object in the database.</returns>
        public virtual ICollection<TEntity> GetAll()
        {
            return Db.Set<TEntity>().ToList();
        }

        /// <summary>
        /// Gets a collection of all objects in the database.
        /// </summary>
        /// <returns>An ICollection of every object in the database.</returns>
        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await Db.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// Returns a single object which matches the provided expression.
        /// </summary>
        /// <param name="match">A Linq expression filter to find a single result.</param>
        /// <returns>A single object which matches the expression filter.
        /// If more than one object is found or if zero are found, null is returned.</returns>
        public virtual TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return Db.Set<TEntity>().SingleOrDefault(match);
        }

        /// <summary>
        /// Returns a single object which matches the provided expression.
        /// </summary>
        /// <param name="match">A Linq expression filter to find a single result.</param>
        /// <returns>A single object which matches the expression filter.
        /// If more than one object is found or if zero are found, null is returned.</returns>
        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await Db.Set<TEntity>().SingleOrDefaultAsync(match);
        }

        /// <summary>
        /// Returns a collection of objects which match the provided expression.
        /// </summary>
        /// <param name="match">A linq expression filter to find one or more results.</param>
        /// <returns>An ICollection of object which match the expression filter.</returns>
        public virtual ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return Db.Set<TEntity>().Where(match).ToList();
        }

        /// <summary>
        /// Returns a collection of objects which match the provided expression.
        /// </summary>
        /// <param name="match">A linq expression filter to find one or more results.</param>
        /// <returns>An ICollection of object which match the expression filter.</returns>
        public virtual async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await Db.Set<TEntity>().Where(match).ToListAsync();
        }

        /// <summary>
        /// Inserts a single object to the database and commits the change.
        /// </summary>
        /// <param name="e">The object to insert.</param>
        /// <returns>The resulting object including its primary key after the insert.</returns>
        public virtual TEntity Add(TEntity e)
        {
            Db.Set<TEntity>().Add(e);
            Db.SaveChanges();
            return e;
        }

        /// <summary>
        /// Inserts a single object to the database and commits the change.
        /// </summary>
        /// <param name="e">The object to insert.</param>
        /// <returns>The resulting object including its primary key after the insert.</returns>
        public virtual async Task<TEntity> AddAsync(TEntity e)
        {
            Db.Set<TEntity>().Add(e);
            await Db.SaveChangesAsync();
            return e;
        }

        /// <summary>
        /// Inserts a collection of objects into the database and commits the changes.
        /// </summary>
        /// <param name="eList">An IEnumerable list of objects to insert.</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys.</returns>
        public virtual IEnumerable<TEntity> AddAll(IEnumerable<TEntity> eList)
        {
            Db.Set<TEntity>().AddRange(eList);
            Db.SaveChanges();
            return eList;
        }

        /// <summary>
        /// Inserts a collection of objects into the database and commits the changes.
        /// </summary>
        /// <param name="eList">An IEnumerable list of objects to insert.</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys.</returns>
        public virtual async Task<IEnumerable<TEntity>> AddAllAsync(IEnumerable<TEntity> eList)
        {
            Db.Set<TEntity>().AddRange(eList);
            await Db.SaveChangesAsync();
            return eList;
        }

        /// <summary>
        /// Updates a single object based on the provided primary key and commits the change.
        /// </summary>
        /// <param name="updated">The updated object to apply to the database.</param>
        /// <param name="key">The primary key of the object to update.</param>
        /// <returns>The resulting updated object.</returns>
        public virtual TEntity Update(TEntity updated, object key)
        {
            if (updated == null)
                return null;

            TEntity existing = Db.Set<TEntity>().Find(key);
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
        public virtual async Task<TEntity> UpdateAsync(TEntity updated, object key)
        {
            if (updated == null)
                return null;

            TEntity existing = await Db.Set<TEntity>().FindAsync(key);
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
        public virtual int Delete(TEntity e)
        {
            Db.Set<TEntity>().Remove(e);
            return Db.SaveChanges();
        }

        /// <summary>
        /// Deletes a single object from the database and commits the change.
        /// </summary>
        /// <param name="e">The object to delete.</param>
        /// <returns>The number of effected objects.</returns>
        public virtual async Task<int> DeleteAsync(TEntity e)
        {
            Db.Set<TEntity>().Remove(e);
            return await Db.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the count of the number of objects in the database.
        /// </summary>
        /// <returns>The count of the number of objects.</returns>
        public virtual int Count()
        {
            return Db.Set<TEntity>().Count();
        }

        /// <summary>
        /// Gets the count of the number of objects in the database.
        /// </summary>
        /// <returns>The count of the number of objects.</returns>
        public virtual async Task<int> CountAsync()
        {
            return await Db.Set<TEntity>().CountAsync();
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