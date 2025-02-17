using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Shoaib_Task.Web.Models;

namespace Shoaib_Task.Web.Controllers
{
    public class ServicesController : Controller
    {
        private readonly HttpClient _httpClient;

        public ServicesController()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7014/") };

        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWT");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var services = await _httpClient.GetFromJsonAsync<List<ServiceModel>>("api/services");
            ViewBag.token = token;
            ViewBag.userid = GetClaimsFromToken(token, ClaimTypes.NameIdentifier);
            return View(services);
        }
        public IActionResult AddService()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddService(AddServiceModel model)
        {
            var token = HttpContext.Session.GetString("JWT");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync("api/services", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Failed";
            return View();
        }

        public static string GetClaimsFromToken(string token,string key)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            return jwtToken.Claims.FirstOrDefault(c => c.Type==key)?.Value;
        }
    }
}
