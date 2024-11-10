using StudentWebApp.Models.Group;
using StudentWebApp.Models.Student;

namespace StudentWebApp.Models.StudentViewModels
{
    public class StudentEditViewmodel
    {
        public StudentDto? Student { get; set; }

        public List<GroupDto> Groups { get; set; }
    }
}
