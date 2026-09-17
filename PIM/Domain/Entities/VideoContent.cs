using System;
using PIM.Domain.Enums;

namespace PIM.Domain.Entities
{
    public class VideoContent : LessonContent
    {
        public VideoContent() { Type = ContentType.Video; }
        public string Url { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public string? Transcript { get; set; }
    }
}
