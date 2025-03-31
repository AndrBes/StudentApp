using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentWebApi.Controllers.Models.Student
{
    public class LoginResponseModel
    {

        public int Status { get; set; }

        public string? Token { get; set; }

        public string? Login { get; set; }
    }
}
