using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Models;
using PortfolioAPI.Services;
using System.Threading.Tasks;
 
namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ContactInfoService _contactInfoService;
 
        public ContactController(ContactInfoService contactInfoService)
        {
            _contactInfoService = contactInfoService;
        }
 
        // POST: /api/contact
        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] ContactInfo contactInfo)
        {
            await _contactInfoService.AddContactAsync(contactInfo);
            return Ok(contactInfo);
        }
    }
}