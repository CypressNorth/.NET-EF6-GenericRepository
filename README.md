.NET-EF6-GenericRepository
==========================

A Generic Entity Framework 6 data repository for .NET 4.5 supporting both sync and async methods

#Usage
Include the BaseService.cs class in your application and set the appropriate namespace.

Inherit from BaseService in your Service classes and initialize the constructor and base constructor with a data context.

```C#
public class CategoryService : BaseService<Category>
{
    public CategoryService(MyDataContext context)
        : base(context)
    { }
}
```

#Overrides
Override the base methods as needed by declaring the same method as *new*

```C#
public new async Task<Category> GetAsync(int id)
{
    return await _context.Categories.SingleOrDefaultAsync(x => x.categoryID == id);
}
```
