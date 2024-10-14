using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Models;
using PortfolioAPI.Services;
using System.Threading.Tasks;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectsService _projectsService;

        public ProjectsController(ProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectsService.GetProjectsAsync();
            return Ok(projects);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] Project project)
        {
            await _projectsService.AddProjectAsync(project);
            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id,  Project project)
        {
            await _projectsService.UpdateProjectAsync(id, project);
            return Ok(project);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _projectsService.DeleteProjectAsync(id);
            return Ok();
        }
    }
}
