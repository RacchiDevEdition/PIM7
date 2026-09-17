using System.Collections.Generic;

namespace PIM.Domain.Entities
{
    public class Student : User
    {
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public virtual ICollection<Progress> ProgressRecords { get; set; } = new List<Progress>();
    }
}
