using System;
using System.Collections.Generic;

namespace PIM.Domain.Entities
{
    public class QuizResult
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid StudentId { get; set; }
        public virtual Student? Student { get; set; }
        public Guid QuizContentId { get; set; }
        public virtual QuizContent? QuizContent { get; set; }
        public int Score { get; set; }
        public DateTimeOffset TakenAt { get; set; } = DateTimeOffset.UtcNow;
        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
