using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagment.BLL.Interfaces;
using ProjectManagment.DAL.Entities;
using ProjectManagment.WEB.Models;

namespace ProjectManagment.WEB.Controllers
{
    public class ProjectController(IService<Project> service, IMapper mapper) : Controller
    {
        private readonly IService<Project> _service = service;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index()
        {
            var projects = _mapper.Map<IEnumerable<ProjectViewModel>>(await _service.GetAll());
            return View("Project", projects);
        }

        public async Task<IActionResult> ProjectWizard(Guid? id = null)
        {
            if(id == null)
            {
                return View("ProjectWizard");
            }
            var project = _mapper.Map<ProjectViewModel>(await _service.Get((Guid)id));
            return View("ProjectWizard", project);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProject(Project project)
        {
            if(project != null)
            {
                if(project.Id == Guid.Empty)
                {
                    await _service.Add(project);
                }
                else
                {
                    await _service.Update(project);
                }
            }
            return RedirectToAction("Index");
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
}
