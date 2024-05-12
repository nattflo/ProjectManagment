
using ProjectManagment.DAL.Entities;

namespace ProjectManagment.DAL.Interfaces;

public interface ICompanyRepository : IRepository<Company>
{
    Task<bool> HasCompanyProjects(Guid id);
}
