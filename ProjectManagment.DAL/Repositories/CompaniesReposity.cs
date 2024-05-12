using Microsoft.EntityFrameworkCore;
using ProjectManagment.DAL.Contexts;
using ProjectManagment.DAL.Entities;
using ProjectManagment.DAL.Interfaces;

namespace ProjectManagment.DAL.Repositories;

public sealed class CompaniesRepository(AppDbContext context) : BaseRepository<Company>(context), ICompanyRepository
{
    public async Task<bool> HasCompanyProjects(Guid id)
    {
        return await _context.Set<Project>().AnyAsync(p => p.ClientId == id || p.ContractorId == id);
    }
}
