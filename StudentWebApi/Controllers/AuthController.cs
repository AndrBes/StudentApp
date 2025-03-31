using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentData;
using StudentWebApi.Configuration;
using StudentWebApi.Controllers.Models;
using StudentWebApi.Controllers.Models.Student;
using StudentWebApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudentWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")] // ДЕФОЛТ ПУТЬ ДО ФУНКЦИИ
public class AuthController(ILogger<StudentController> _logger,
    StudentContext _context,
    IMapper _mapper) : ControllerBase
{
    [HttpPost]
    public LoginResponseModel? Login([FromBody] LoginRequestModel model)
    {
        var user = _context.Users.FirstOrDefault(x => x.Login == model.Login && x.Password == model.Password);
        //var users = _context.Users.ToList();
        //_context.Users.Add(new User
        //{
        //    Login = "admin",
        //    Password = "123"
        //});
        //_context.SaveChanges();

        if (user == null)
            return new LoginResponseModel { Status = 1 };

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, model.Login) };
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new LoginResponseModel { Status = 0, Token = token , Login = user.Login};
    }
}
