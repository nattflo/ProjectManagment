using Microsoft.EntityFrameworkCore;
using ProjectManagment.DAL.Contexts;
using ProjectManagment.DAL.Entities;
using ProjectManagment.DAL.Interfaces;

namespace ProjectManagment.DAL.Repositories;

public sealed class EmployeesRepository(AppDbContext context) : BaseRepository<Employee>(context), IEmployeeRepository
{
    public async Task<bool> HasEmployeeManagedProjects(Guid id)
    {
        return await _context.Set<Project>().AnyAsync(p => p.ProjectManagerId == id);
    }

    public async Task<bool> HasEmployeeProjects(Guid id)
    {
        return await _context.Set<Project>().AnyAsync(p => p.EmployeeIds.Any(eid => eid == id));
    }
}
