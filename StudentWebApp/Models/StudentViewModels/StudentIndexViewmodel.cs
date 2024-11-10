using StudentWebApp.Models.Group;
using StudentWebApp.Models.Student;

namespace StudentWebApp.Models.StudentViewModels
{
    public class StudentIndexViewmodel
    {
        public List<StudentDto> Students { get; set; }

        public List<GroupDto> Groups { get; set; }
    }
}
