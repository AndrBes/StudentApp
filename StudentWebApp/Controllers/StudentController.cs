using Microsoft.AspNetCore.Mvc;
using StudentWebApp.Models;
using StudentWebApp.Models.Student;
using StudentWebApp.Models.StudentViewModels;
using StudentWebApp.Services;
using System.Diagnostics;

namespace StudentWebApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly StudentApiService _studentApiService;

        public StudentController(ILogger<StudentController> logger, StudentApiService studentApiService)
        {
            _logger = logger;
            _studentApiService = studentApiService;
        }
        [HttpPost]
        public async Task<IActionResult> Index(StudentFilterDto filter)
        {
            var viewModel = new StudentIndexViewmodel
            {
                Students = await _studentApiService.GetStudents(filter),
                Groups = await _studentApiService.GetGroups()
            };
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new StudentIndexViewmodel
            {
                Students = await _studentApiService.GetStudents(new StudentFilterDto()),
                Groups = await _studentApiService.GetGroups()
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TestPage()
        {
            return View();
        }
        #region Добавление студента
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new StudentAddViewmodel
            {
                Groups = await _studentApiService.GetGroups(),
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Add(StudentAddDto dto)
        {
            var viewModel = new StudentAddViewmodel
            {
                Groups = await _studentApiService.GetGroups(),
                Student = dto
            };
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var result = await _studentApiService.AddStudent(dto);

            if (!result)
            {
                ModelState.AddModelError("api_error", "Ошибка валидации");

                return View(viewModel);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Удаление студента
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _studentApiService.RemoveStudent(id);

            TempData["Message"] = result ? "Пользователь был удален" : "Ошибка удаления";

            return RedirectToAction("Index");
        }
        #endregion

        #region Редактирование студента
        [HttpPost]
        public async Task<IActionResult> Edit(StudentEditDto dto)
        {
            var viewModel = new StudentEditViewmodel
            {
                Groups = await _studentApiService.GetGroups()
            };
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await _studentApiService.EditStudent(dto);

            if (!result)
            {
                ModelState.AddModelError("api_error", "Ошибка валидации данных");
                return View(viewModel);
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = new StudentEditViewmodel
            {
                Groups = await _studentApiService.GetGroups(),
                Student = await _studentApiService.GetStudent(id)
            };

            return View(viewModel);
        }
        #endregion

        #region Получение групп

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var viewModel = new StudentAddViewmodel
            {
                Groups = await _studentApiService.GetGroups()
            };
            return View(viewModel);
        }
        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
