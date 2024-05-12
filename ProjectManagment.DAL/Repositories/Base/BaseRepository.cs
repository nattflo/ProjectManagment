using Microsoft.EntityFrameworkCore;
using ProjectManagment.DAL.Contexts;
using ProjectManagment.DAL.Entities;

namespace ProjectManagment.DAL.Repositories;

public abstract class BaseRepository<T>(AppDbContext context) : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _context = context;

    public async Task Add(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await _context.Set<T>().Where(e => e.Id == id).ExecuteDeleteAsync();
    }

    public IEnumerable<T> Find(Func<T, bool> predicate)
    {
        return _context.Set<T>().AsNoTracking().Where(predicate);
    }

    public async Task<T?> Get(Guid id)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task Update(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }
}
