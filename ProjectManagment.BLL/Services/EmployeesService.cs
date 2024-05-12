using ProjectManagment.BLL.Services.Base;
using ProjectManagment.DAL.Entities;
using ProjectManagment.DAL.Interfaces;

namespace ProjectManagment.BLL.Services;

public class EmployeesService(IEmployeeRepository repository) : BaseService<Employee, IEmployeeRepository>(repository)
{
    public override IEnumerable<Employee> GetByName(string name)
    {
        return _repository.Find(e => e.FirstName.Contains(name, StringComparison.CurrentCultureIgnoreCase) || e.LastName.Contains(name, StringComparison.CurrentCultureIgnoreCase));
    }
    public override async Task<Result> Delete(Guid id)
    {
        if (!await IsEntityExist(id))
            return Result.Failure("Employee does not exist!");

        if (await _repository.HasEmployeeProjects(id) || await _repository.HasEmployeeManagedProjects(id))
            return Result.Failure("Employee is currently working on a project or is a manager!");

        await _repository.Delete(id);
        return Result.Success();
    }
}
