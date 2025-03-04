using System.ComponentModel.DataAnnotations;

namespace Simple_CRUD.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public bool Subscribed { get; set; }
    }
}
