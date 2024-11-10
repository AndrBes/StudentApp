using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentWebApi.Controllers.Models.Student
{
    public class StudentGetDto
    {
        public int StudentId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Midname { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string GroupName { get; set; }

        public int GroupId { get; set; }
    }
}
