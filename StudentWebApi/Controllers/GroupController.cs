using Microsoft.AspNetCore.Mvc;
using StudentData;
using StudentWebApi.Controllers.Models;

namespace StudentWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")] // ДЕФОЛТ ПУТЬ ДО ФУНКЦИИ
    public class GroupController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly StudentContext _context;

        public GroupController(ILogger<StudentController> logger, StudentContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpPut]
        public Group Add([FromBody] Group model)
        {
            _context.Groups.Add(model);
            _context.SaveChanges();
            return model;
        }
        [HttpGet]
        public Group? Get(int id)
        {
            var group = _context.Groups.FirstOrDefault(x => x.Id == id);
            return group;
        }
        [HttpGet]
        // [Route("[action]")] // ДОБАВИТЬ ПУТЬ
        public List<Group> GetAll()
        {
            var groups = _context.Groups.ToList();

            return groups;
        }
        [HttpDelete]
        public void Delete(int id)
        {
            var group = _context.Groups.FirstOrDefault(x => x.Id == id);
            if (group != null)
            {
                _context.Groups.Remove(group);
                _context.SaveChanges();
            }
        }
        [HttpPost]
        public Group? Post([FromBody] Group model)
        {
            var group = _context.Groups.FirstOrDefault(x => x.Id == model.Id);
            if (group != null)
            {
                group.Name = model.Name;
                group.Id = model.Id;
                _context.Groups.Update(group);
                _context.SaveChanges();
            }

            return group;
        }
    }
}
