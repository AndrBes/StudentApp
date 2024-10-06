using System.ComponentModel.DataAnnotations;

namespace StudentData
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string Midname { get; set; }
        public int GroupId { get; set; }
    }
}
