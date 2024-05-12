using System.ComponentModel.DataAnnotations;

namespace ProjectManagment.WEB.Models;

public class EmployeeViewModel
{
    public Guid Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}
