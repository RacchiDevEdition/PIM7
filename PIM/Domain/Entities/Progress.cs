using System;

namespace PIM.Domain.Entities
{
    public class Progress
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid StudentId { get; set; }
        public virtual Student? Student { get; set; }
        public Guid LessonId { get; set; }
        public virtual Lesson? Lesson { get; set; }
        public bool Completed { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public int? Score { get; set; }
    }
}
