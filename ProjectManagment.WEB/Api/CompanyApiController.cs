using Microsoft.AspNetCore.Mvc;
using ProjectManagment.DAL.Entities;
using ProjectManagment.BLL.Interfaces;

namespace ProjectManagment.WEB.Api;

[ApiController]
[Route("api/Company")]
public class CompanyApiController(IService<Company> service) : ControllerBase
{
    private readonly IService<Company> _service = service;

    [HttpGet("GetByName")]
    public ActionResult<Company> GetByName(string search)
    {
        return Ok(_service.GetByName(search));
    }
}
