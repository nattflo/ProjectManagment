using ProjectManagment.BLL.Interfaces;
using ProjectManagment.DAL;
using ProjectManagment.DAL.Entities;

namespace ProjectManagment.BLL.Services.Base;

public abstract class BaseService<TEntity, TRepository>(TRepository repository) : IService<TEntity> where TEntity : BaseEntity where TRepository : IRepository<TEntity>
{
    protected readonly TRepository _repository = repository;

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<TEntity?> Get(Guid id)
    {
        return await _repository.Get(id);
    }

    public async Task Add(TEntity entity)
    {
        await _repository.Add(entity);
    }

    public async Task Update(TEntity entity)
    {
        if (await IsEntityExist(entity.Id))
            await _repository.Update(entity);
    }
    public virtual async Task<Result> Delete(Guid id) 
    {
        if (!await IsEntityExist(id))
            return Result.Failure($"{nameof(TEntity)} does not exist!");

        await _repository.Delete(id);
        return Result.Success();
    }
    protected async Task<bool> IsEntityExist(Guid id) => await _repository.Get(id) != null;

    public abstract IEnumerable<TEntity> GetByName(string name);
}
