using Fleet.context;

namespace Fleet.data;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly FleetContext Context;

    public BaseRepository(FleetContext context)
    {
        Context = context;
    }

    public void Add(T entity)
    {
        Context.Set<T>().Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        Context.Set<T>().AddRange(entities);
    }

    public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
    {
       return Context.Set<T>().Where(predicate);
    }

    public T Get(int id)
    {
        return Context.Set<T>().Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return Context.Set<T>().ToList();
    }

    public void Remove(T entity)
    {
        Context.Set<T>().Remove(entity);
    }

    public void RemoveRange(T entities)
    {
        Context.Set<T>().RemoveRange(entities);
    }
}