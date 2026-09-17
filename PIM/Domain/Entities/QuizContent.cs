using System.Collections.Generic;
using PIM.Domain.Enums;

namespace PIM.Domain.Entities
{
    public class QuizContent : LessonContent
    {
        public QuizContent() { Type = ContentType.Quiz; }
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
