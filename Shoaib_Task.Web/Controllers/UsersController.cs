using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Shoaib_Task.Web.Models;

namespace Shoaib_Task.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient _httpClient;

        public UsersController()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7014/") };

        }
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWT");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var services = await _httpClient.GetFromJsonAsync<List<UserModel>>("api/users");
            return View(services);
        }
    }
}
