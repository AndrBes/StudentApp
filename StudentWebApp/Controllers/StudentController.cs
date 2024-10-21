using Microsoft.AspNetCore.Mvc;
using StudentWebApp.Models;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentWebApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:5254/Student/GetAll");
            var responseText = await response.Content.ReadAsStringAsync();

            var responseData = JsonSerializer.Deserialize<List<StudentDto>>(responseText, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            
            return View(responseData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TestPage()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(StudentAddDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsJsonAsync("http://localhost:5254/Student/Add", dto);

            if(!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("api_error", "Ошибка валидации");
                
                return View(dto);
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
