using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Shoaib_Task.Web.Models;

namespace Shoaib_Task.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Auth");
            ViewBag.role = GetClaimsFromToken(token, ClaimTypes.Role);
            return View();
        }
        public static string GetClaimsFromToken(string token, string key)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            return jwtToken.Claims.FirstOrDefault(c => c.Type == key)?.Value;
        }
    }
}
