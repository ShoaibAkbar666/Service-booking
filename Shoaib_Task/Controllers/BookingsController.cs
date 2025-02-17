using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoaib_Task.Entitties;
using Shoaib_Task.Models;

namespace Shoaib_Task.Controllers
{

    [Route("api/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpPost]
        public IActionResult BookService(Booking booking)
        {
            booking.BookingDate = DateTime.UtcNow;
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return Ok(booking);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/approve")]
        public IActionResult ApproveService(int id)
        {
            var service = _context.Bookings.Find(id);
            if (service == null) return NotFound();

            service.IsApproved = true;
            _context.SaveChanges();
            return Ok(service);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/reject")]
        public IActionResult RejectService(int id)
        {
            var service = _context.Bookings.Find(id);
            if (service == null) return NotFound();

            service.IsApproved = false;
            _context.SaveChanges();
            return Ok(service);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetApprovedServices()
        {
            return Ok(_context.Bookings.Where(a => a.IsApproved != true).ToList().Select(a => new ViewBookingModel
            {
                Id = a.Id,  
                Servicename = _context.Services.Find(a.ServiceId)?.Name,
                Username= _context.Users.Find(a.UserId)?.Name,
                BookingDate = a.BookingDate
            }));
        }
    }
}
