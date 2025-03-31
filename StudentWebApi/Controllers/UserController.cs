using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentData;

namespace StudentWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")] // ДЕФОЛТ ПУТЬ ДО ФУНКЦИИ
    public class UserController(ILogger<StudentController> _logger,
    StudentContext _context) : ControllerBase
    {

        [HttpGet]
        public User Info()
        {
            var user = _context.Users.FirstOrDefault(x => x.Login == User.Identity.Name);

            return user;
        }
    }
}
