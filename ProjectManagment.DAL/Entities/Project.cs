namespace ProjectManagment.DAL.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; }
    public Guid ProjectManagerId { get; set; }
    public IList<Guid> EmployeeIds { get; set; }
    public Guid ClientId { get; set; }
    public Guid ContractorId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Priority Priority { get; set; }
}
