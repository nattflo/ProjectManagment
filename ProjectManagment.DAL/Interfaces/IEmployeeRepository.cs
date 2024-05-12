using ProjectManagment.DAL.Entities;

namespace ProjectManagment.DAL.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<bool> HasEmployeeProjects(Guid id);
    Task<bool> HasEmployeeManagedProjects(Guid id);
}
