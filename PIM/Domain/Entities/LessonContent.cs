using System;
using PIM.Domain.Enums;

namespace PIM.Domain.Entities
{
    public abstract class LessonContent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Order { get; set; }
        public ContentType Type { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
