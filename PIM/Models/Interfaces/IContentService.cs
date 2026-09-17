using System;
using PIM.Domain.Entities;

namespace PIM.Models.Interfaces
{
    public interface IContentService
    {
        LessonContent Create(LessonContent content);
        LessonContent? GetById(Guid id);
        void Delete(Guid id);
    }
}
