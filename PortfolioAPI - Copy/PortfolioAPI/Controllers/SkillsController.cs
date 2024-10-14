using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Models;
using PortfolioAPI.Services;
using System.Threading.Tasks;
 
namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly SkillsService _skillsService;
 
        public SkillsController(SkillsService skillsService)
        {
            _skillsService = skillsService;
        }
 
        // GET: /api/skills
        [HttpGet]
        public async Task<IActionResult> GetSkills()
        {
            var skills = await _skillsService.GetSkillsAsync();
            return Ok(skills);
        }
 
        // POST: /api/skills
        // [HttpPost]
        // public async Task<IActionResult> AddSkill([FromBody] Skill skill)
        // {
        //     await _skillsService.AddSkillAsync(skill);
        //     return Ok(skill);
        // }
    }
}