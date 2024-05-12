using ProjectManagment.BLL.Services.Base;
using ProjectManagment.DAL.Entities;
using ProjectManagment.DAL.Interfaces;

namespace ProjectManagment.BLL.Services;

public class CompaniesService(ICompanyRepository repository) : BaseService<Company, ICompanyRepository>(repository)
{
    public override IEnumerable<Company> GetByName(string name)
    {
        return _repository.Find(e => e.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));
    }

    public override async Task<Result> Delete(Guid id)
    {
        if(!await IsEntityExist(id))
            return Result.Failure("Сompany does not exist!");

        if (await _repository.HasCompanyProjects(id))
            return Result.Failure("Сompany is currently involved in the project!");

        await _repository.Delete(id);
        return Result.Success();
    }
}
