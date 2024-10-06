using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentData
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Midname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [ForeignKey(nameof(Group))]
        public int GroupId { get; set; }
        
        public Group? Group { get; set; }
    }
}
