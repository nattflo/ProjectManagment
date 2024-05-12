using Microsoft.AspNetCore.Mvc;
using ProjectManagment.BLL.Interfaces;
using ProjectManagment.DAL.Entities;

namespace ProjectManagment.WEB.API;

[ApiController]
[Route("api/Employee")]
public class EmployeeApiController(IService<Employee> service) : ControllerBase
{
    private readonly IService<Employee> _service = service;

    [HttpGet("GetByName")]
    public ActionResult<Employee> GetByName(string search)
    {
        return Ok(_service.GetByName(search));
    }
}
