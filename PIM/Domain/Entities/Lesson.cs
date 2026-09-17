using System;
using System.Collections.Generic;

namespace PIM.Domain.Entities
{
    public class Lesson
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CourseId { get; set; }
        public virtual Course? Course { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Order { get; set; }
        public virtual ICollection<LessonContent> Contents { get; set; } = new List<LessonContent>();
    }
}
