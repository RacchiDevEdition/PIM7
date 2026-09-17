using PIM.Domain.Enums;

namespace PIM.Domain.Entities
{
    public class ArticleContent : LessonContent
    {
        public ArticleContent() { Type = ContentType.Article; }
        public string Body { get; set; } = string.Empty; // markdown or html
        public string? Summary { get; set; }
    }
}
