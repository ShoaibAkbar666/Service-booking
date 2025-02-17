using Microsoft.AspNetCore.Mvc;
using Shoaib_Task.Web.Models;

namespace Shoaib_Task.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthController()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7014/") };
        }

        public IActionResult Login()
        {
            HttpContext.Session.Remove("JWT");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenModel>();
                HttpContext.Session.SetString("JWT", result.token);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Invalid login credentials";
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Login", "Auth");
            }
            ViewBag.Error = "Invalid login credentials";
            return View();
        }

    }
}
