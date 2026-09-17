using System;
using PIM.Domain.Enums;

namespace PIM.Domain.Entities
{
    public class Enrollment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid StudentId { get; set; }
        public virtual Student? Student { get; set; }
        public Guid CourseId { get; set; }
        public virtual Course? Course { get; set; }
        public DateTimeOffset EnrolledAt { get; set; } = DateTimeOffset.UtcNow;
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;
    }
}
