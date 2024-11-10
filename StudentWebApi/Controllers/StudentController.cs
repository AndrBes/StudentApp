using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudentData;
using StudentWebApi.Configuration;
using StudentWebApi.Controllers.Models.Student;
using StudentWebApi.Services;

namespace StudentWebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")] // ДЕФОЛТ ПУТЬ ДО ФУНКЦИИ
public class StudentController(ILogger<StudentController> _logger,
    StudentContext _context,
    IMapper _mapper,
    SingletonService _singletonService,
    TransientService _transientService,
    ScopedService _scopedService,
    TransientService2 transientService2,
    UserVisitService userVisitService,
    IOptions<MailConfig> mailOptions) : ControllerBase
{
    [HttpGet]
    public string Test()
    {
        // Отправляем почту
        var mail = mailOptions;

        _singletonService.Counter++;
        _scopedService.Counter++;
        _transientService.Counter++;
        userVisitService.UserVisits.Add(DateTime.Now);
        userVisitService.UserVisits = userVisitService.UserVisits.Where(x => x >= DateTime.Now.AddSeconds(-10)).ToList();
        _logger.LogTrace("LogTrace");
        _logger.LogDebug("LogDebug");
        _logger.LogInformation("LogInformation");
        _logger.LogWarning("LogWarning");
        _logger.LogError("LogError");
        _logger.LogCritical("LogCritical");

        return $"Singleton обращения: {_singletonService.Counter}, Scoped обращения: {_scopedService.Counter}, Transient обращения: {_transientService.Counter}, Количество онлайн: {userVisitService.UserVisits.Count}";
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
    [HttpPost]
    // [Route("[action]")] // ДОБАВИТЬ ПУТЬ
    public List<StudentGetDto> GetAll(StudentFilterDto model)
    {




        var query = _context.Students.AsQueryable();

        if (!string.IsNullOrEmpty(model.LastName)) query = query.Where(x =>  x.LastName.Contains(model.LastName));
        if (!string.IsNullOrEmpty(model.FirstName)) query = query.Where(x => x.FirstName.Contains(model.FirstName));
        if (!string.IsNullOrEmpty(model.Midname)) query = query.Where(x => x.Midname.Contains(model.Midname));
        if (!string.IsNullOrEmpty(model.Email)) query = query.Where(x => x.Email.Contains(model.Email));
        if (model.GroupId != null) query = query.Where(x => x.GroupId == model.GroupId);

        return query
            .Include(p => p.Group)
            //Нужен чтобы не отправлять в запросе пароль и другие ненужные данные(чек консоль)
            .ProjectTo<StudentGetDto>(_mapper.ConfigurationProvider)
            .ToList();
        //var studentsDto = _mapper.Map<List<StudentGetDto>>(students);

        //    students.Select(x => new StudentGetDto
        //{
        //    Id = x.Id,
        //    LastName = x.LastName,
        //    FirstName = x.FirstName,
        //    Midname = x.Midname,
        //    GroupName = x.Group.Name,
        //    Email = x.Email,
        //}).ToList();

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
