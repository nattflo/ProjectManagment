using ProjectManagment.DAL;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagment.WEB.Models;

[Serializable]
public class ProjectViewModel
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public IEnumerable<EmployeeViewModel> Employees { get; set; }
    [Required]
    public EmployeeViewModel ProjectManager { get; set; }
    [Required]
    public CompanyViewModel Client { get; set; }
    [Required]
    public CompanyViewModel Contractor { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    [Required]
    public Priority Priority { get; set; }
}
