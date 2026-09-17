using System;

namespace PIM.Domain.Entities
{
    public class Choice
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
