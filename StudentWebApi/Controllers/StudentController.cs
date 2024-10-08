using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;

        public StudentController(ILogger<StudentController> logger, StudentContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }
        [HttpPut]
        public Student Add([FromBody] StudentAddDto model)
        {
            var student = _mapper.Map<Student>(model);
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }
        [HttpGet]
        public StudentGetDto? Get(int id)
        {
            var student = _context.Students
                .Include(p => p.Group)
                .FirstOrDefault(x => x.Id == id);
            var studentDto = _mapper.Map<StudentGetDto>(student);
            return studentDto;
        }
        [HttpGet]
        // [Route("[action]")] // ДОБАВИТЬ ПУТЬ
        public List<StudentGetDto> GetAll()
        {
            var students = _context.Students
                .Include(p => p.Group)
                // Нужен чтобы не отправлять в запросе пароль и другие ненужные данные (чек консоль)
                .ProjectTo<StudentGetDto>(_mapper.ConfigurationProvider)
                .ToList();
            var studentsDto = _mapper.Map<List<StudentGetDto>>(students);

            //    students.Select(x => new StudentGetDto
            //{
            //    Id = x.Id,
            //    LastName = x.LastName,
            //    FirstName = x.FirstName,
            //    Midname = x.Midname,
            //    GroupName = x.Group.Name,
            //    Email = x.Email,
            //}).ToList();

            return studentsDto;
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
