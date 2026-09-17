using System.Collections.Generic;

namespace PIM.Domain.Entities
{
    public class Teacher : User
    {
        public virtual ICollection<Course> CoursesCreated { get; set; } = new List<Course>();
    }
}
