using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagment.BLL.Interfaces;
using ProjectManagment.DAL.Entities;
using ProjectManagment.WEB.Models;

namespace ProjectManagment.WEB.Controllers
{
    public class EmployeeController(IService<Employee> service, IMapper mapper) : Controller
    {
        private readonly IService<Employee> _service = service;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index()
        {
            var employees = _mapper.Map<IEnumerable<EmployeeViewModel>>(await _service.GetAll());
            return View("employee", employees);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            await _service.Add(_mapper.Map<Employee>(employee));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeViewModel employee)
        {
            await _service.Update(_mapper.Map<Employee>(employee));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeModal(Guid id)
        {
            var employee = await _service.Get(id);
            if (employee == null) return PartialView("_EmployeeModalFormPartial", new EmployeeViewModel());
            else return PartialView("_EmployeeModalFormPartial", _mapper.Map<EmployeeViewModel>(employee));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var result = await _service.Delete(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok();
        }
    }
}
