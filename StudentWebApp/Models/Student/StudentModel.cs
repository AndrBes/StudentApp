using System.ComponentModel.DataAnnotations;

namespace StudentWebApp.Models.Student
{
    public class StudentModel
    {
        [Required(ErrorMessage = "Заполните фамилию")]
        [StringLength(50, ErrorMessage = "Некорректная фамилия")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Заполните имя")]
        [StringLength(50, ErrorMessage = "Некорректное имя")]
        public string FirstName { get; set; }
        public string MidName { get; set; }
    }
}
