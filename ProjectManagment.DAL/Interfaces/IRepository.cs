using ProjectManagment.DAL.Entities;

namespace ProjectManagment.DAL;

public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> Get(Guid id);
    IEnumerable<T> Find(Func<T, Boolean> predicate);
    Task Add(T item);
    Task Update(T item);
    Task Delete(Guid id);
}
