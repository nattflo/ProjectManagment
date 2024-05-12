using ProjectManagment.DAL.Entities;

namespace ProjectManagment.BLL.Interfaces;

public interface IService<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> Get(Guid id);
    IEnumerable<T> GetByName(string name);
    Task Add(T item);
    Task Update(T item);
    Task<Result> Delete(Guid id);
}
