using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentWebApi.Controllers.Models.Student
{
    public class LoginRequestModel
    {

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
