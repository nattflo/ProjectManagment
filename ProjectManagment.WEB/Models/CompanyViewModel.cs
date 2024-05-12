using System.ComponentModel.DataAnnotations;

namespace ProjectManagment.WEB.Models;

public class CompanyViewModel
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }

}
