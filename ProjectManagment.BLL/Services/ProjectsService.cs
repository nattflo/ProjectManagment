using ProjectManagment.DAL.Entities;
using ProjectManagment.DAL.Interfaces;
using ProjectManagment.BLL.Services.Base;

namespace ProjectManagment.BLL.Services;

public class ProjectsService(IProjectRepository repository) : BaseService<Project, IProjectRepository>(repository)
{
    public override IEnumerable<Project> GetByName(string name)
    {
        return _repository.Find(e => e.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));
    }
}
