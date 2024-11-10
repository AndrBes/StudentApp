namespace StudentWebApp.Models.Student
{
    public class StudentEditDto
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Midname { get; set; }

        public string? Email { get; set; }
        public string Password { get; set; }

        public string GroupId { get; set; }
    }
}
