using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentWebApi.Controllers.Models.Student
{
    public class StudentAddDto
    {

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Midname { get; set; }

        public string Email { get; set; }

        public int GroupId { get; set; }

        public string Password { get; set; }
    }
}
