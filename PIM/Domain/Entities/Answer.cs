using System;
using System.Collections.Generic;

namespace PIM.Domain.Entities
{
    public class Answer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid QuestionId { get; set; }
        public virtual Question? Question { get; set; }
        public virtual ICollection<Guid> SelectedChoiceIds { get; set; } = new List<Guid>();
        public string? FreeTextAnswer { get; set; }
    }
}
