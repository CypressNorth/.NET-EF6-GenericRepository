(Improved) .NET-EF6-GenericRepository
=====================================

An enhanced version of "Generic Entity Framework 6 data repository for .NET 4.5 supporting both sync and async methods".

#Credits
Many thanks and appreciation to the main developer of the project: [Matt Mombrea](https://github.com/mombrea)

#Usage
* Reference Data.Repository in your project, then bring BaseRepository class into your application scope to consume its functionality. 

```C#
public class CategoryService : BaseRepository<Category,int>
{
    public CategoryService(AppDbContext context)
        : base(context)
    { }
}
```

* Inherit from BaseRepository in your Repository or Service classes and initialize the constructor and base constructor with an optional data context.

```C#
public class ComplexRepository<TEntity, TKey> : BaseRepository<TEntity, TKey> where TEntity : class
{
    public ComplexRepository(AppDbContext context = null)
        : base(context)
    { }
}
```

#Overrides
- All methods in BaseRepository are marked virtual to overriding convenience.

```C#
public override async Task<Category> GetAsync(TKey id)
{
    return await Db.Categories.SingleOrDefaultAsync(x => x.categoryID == id);
}
```

- IRepository interface represents a "contract" that can be implemented or used to extend the core repository classes method, etc.
