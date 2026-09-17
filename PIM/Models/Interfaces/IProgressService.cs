using System;
using PIM.Domain.Entities;

namespace PIM.Models.Interfaces
{
    public interface IProgressService
    {
        Progress ReportProgress(Guid studentId, Guid lessonId, bool completed, int? score = null);
        Progress? GetByStudentAndLesson(Guid studentId, Guid lessonId);
    }
}
