using System;
using System.Collections.Generic;
using PIM.Domain.Enums;

namespace PIM.Domain.Entities
{
    public class Question
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; } = string.Empty;
        public QuestionType Type { get; set; }
        public virtual ICollection<Choice> Choices { get; set; } = new List<Choice>();
    }
}
