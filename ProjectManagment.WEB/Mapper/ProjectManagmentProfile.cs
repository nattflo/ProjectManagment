using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManagment.BLL.Services.Base;
using ProjectManagment.DAL.Entities;
using ProjectManagment.WEB.Models;

namespace ProjectManagment.WEB.Mapper;

public class ProjectManagmentProfile : Profile
{
    public ProjectManagmentProfile()
    {
        CreateMap<Employee, EmployeeViewModel>();
        CreateMap<EmployeeViewModel, Employee>();
        CreateMap<Company, CompanyViewModel>();
        CreateMap<CompanyViewModel, Company>();
        CreateMap<Project, ProjectViewModel>()
            .ForMember(p => p.ProjectManager, opt => opt.Ignore())
            .ForMember(p => p.Client, opt => opt.Ignore())
            .ForMember(p => p.Contractor, opt => opt.Ignore())
            .AfterMap<SetProjectViewModelAction>();

        CreateMap<ProjectViewModel, Project>()
            .ForMember(p => p.ClientId, opt => opt.MapFrom(p => p.Client.Id))
            .ForMember(p => p.ContractorId, opt => opt.MapFrom(p => p.Contractor.Id))
            .ForMember(p => p.ProjectManagerId, opt => opt.MapFrom(p => p.ProjectManager.Id))
            .ForMember(p => p.EmployeeIds, opt => opt.MapFrom(p => p.Employees.Select(e => e.Id)));
    }
}
