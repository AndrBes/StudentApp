using Microsoft.AspNetCore.Mvc;
using StudentData;
using StudentWebApp.Models;
using System.Diagnostics;

namespace StudentWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentContext _context;

        public HomeController(ILogger<HomeController> logger, StudentContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            List<Student> students = _context.Students.OrderBy(x => x.Id).ToList();

            return View(students);
        }
        public IActionResult Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            if(student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return RedirectToAction("Privacy");
        }
        public IActionResult Edit(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            return View(student);
        }
        //[HttpDelete] // удаление данных
        //[HttpGet] // получение данных
        //[HttpPut] // добавление данных
        //[HttpPatch] // изменение данных
        //[HttpHead] // отправка заголовков без тела
        //[HttpOptions] // получение настроек коммуникаций (http методы)
        
        
        [HttpPost] // получение данных с параметром
        public IActionResult Edit(int id, string firstName, string lastName, string midName)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            
            student.LastName = lastName;
            student.FirstName = firstName;
            student.Midname = midName;

            _context.Students.Update(student);
            _context.SaveChanges();

            return RedirectToAction("Privacy");
        }

        public IActionResult TestPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TestPage(StudentModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var student = new Student()
            {
                GroupId = 1,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Midname = model.MidName
            };

            _context.Students.Add(student);
            _context.SaveChanges();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
