using ProjectManagment.BLL.Services.Base;
using ProjectManagment.DAL.Entities;
using ProjectManagment.DAL.Interfaces;

namespace ProjectManagment.BLL.Tests.Services.Base;

public sealed class TestService(ICompanyRepository repository) : BaseService<Company, ICompanyRepository>(repository)
{
    public override IEnumerable<Company> GetByName(string name)
    {
        return Array.Empty<Company>();
    }
}
