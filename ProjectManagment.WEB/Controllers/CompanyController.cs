using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagment.BLL.Interfaces;
using ProjectManagment.DAL.Entities;
using ProjectManagment.WEB.Models;

namespace ProjectManagment.WEB.Controllers;

public class CompanyController(IService<Company> service, IMapper mapper) : Controller
{
    private readonly IService<Company> _service = service;
    private readonly IMapper _mapper = mapper;

    public async Task<IActionResult> Index()
    {
        var companies = _mapper.Map<IEnumerable<CompanyViewModel>>(await _service.GetAll());
        return View("Company", companies);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CompanyViewModel company)
    {
        await _service.Add(_mapper.Map<Company>(company));
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Update(CompanyViewModel company)
    {
        await _service.Update(_mapper.Map<Company>(company));
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> CompanyModal(Guid id)
    {
        var company = await _service.Get(id);
        if (company == null) return PartialView("_CompanyModalFormPartial", new CompanyViewModel());
        else return PartialView("_CompanyModalFormPartial", _mapper.Map<CompanyViewModel>(company));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _service.Delete(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        return Ok();
    }
}
