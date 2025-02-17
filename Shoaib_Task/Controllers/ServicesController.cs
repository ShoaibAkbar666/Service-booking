using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoaib_Task.Entitties;
using Shoaib_Task.Models;

namespace Shoaib_Task.Controllers
{
    
    [Route("api/services")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServicesController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddService(AddServiceModel service)
        {
            _context.Services.Add(new Service
            {
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,

            });
            _context.SaveChanges();
            return Ok(service);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetApprovedServices()
        {
            return Ok(_context.Services.ToList());
        }
    }
}
