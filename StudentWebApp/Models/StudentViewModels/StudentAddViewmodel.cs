using StudentWebApp.Models.Group;
using StudentWebApp.Models.Student;

namespace StudentWebApp.Models.StudentViewModels
{
    public class StudentAddViewmodel
    {
        public StudentAddDto Student { get; set; }

        public List<GroupDto> Groups { get; set; }
    }
}
