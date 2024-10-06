using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentData;
using StudentWebApi.Controllers.Models;

namespace StudentWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")] // ДЕФОЛТ ПУТЬ ДО ФУНКЦИИ
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly StudentContext _context;

        public StudentController(ILogger<StudentController> logger, StudentContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpPut]
        public Student Add([FromBody] Student model)
        {
            _context.Students.Add(model);
            _context.SaveChanges();
            return model;
        }
        [HttpGet]
        public Student? Get(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            return student;
        }
        [HttpGet]
        // [Route("[action]")] // ДОБАВИТЬ ПУТЬ
        public List<Student> GetAll()
        {
            var students = _context.Students
                .Include(p => p.Group)
                .ToList();

            return students;
        }
        [HttpDelete]
        public void Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
        [HttpPost]
        public Student? Post([FromBody] Student model)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == model.Id);
            if (student != null)
            {
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.Midname = model.Midname;
                student.GroupId = model.GroupId;

                _context.Students.Update(student);
                _context.SaveChanges();
            }

            return student;
        }
    }
}
