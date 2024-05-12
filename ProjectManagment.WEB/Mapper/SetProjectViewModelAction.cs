using AutoMapper;
using ProjectManagment.BLL.Interfaces;
using ProjectManagment.DAL.Entities;
using ProjectManagment.WEB.Models;

namespace ProjectManagment.WEB.Mapper;

public class SetProjectViewModelAction(IService<Employee> employeeService, IService<Company> companyService, IMapper mapper) : IMappingAction<Project, ProjectViewModel>
{
    private readonly IService<Employee> _employeeService = employeeService;
    private readonly IService<Company> _companyService = companyService;
    private readonly IMapper _mapper = mapper;

    public void Process(Project source, ProjectViewModel destination, ResolutionContext context)
    {
        destination.Client = _mapper.Map<CompanyViewModel>(_companyService.Get(source.ClientId).Result);
        destination.Contractor = _mapper.Map<CompanyViewModel>(_companyService.Get(source.ContractorId).Result);
        destination.ProjectManager = _mapper.Map<EmployeeViewModel>(_employeeService.Get(source.ProjectManagerId).Result);
        destination.Employees = _mapper.Map<IEnumerable<EmployeeViewModel>>(source.EmployeeIds.Select(e =>_employeeService.Get(e).Result));
    }
}
