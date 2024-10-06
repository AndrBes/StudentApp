using System.ComponentModel.DataAnnotations;

namespace StudentData
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
