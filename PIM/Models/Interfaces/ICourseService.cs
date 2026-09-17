using System;
using System.Collections.Generic;
using PIM.Domain.Entities;

namespace PIM.Models.Interfaces
{
    public interface ICourseService
    {
        Course Create(Course course);
        Course? GetById(Guid id);
        IEnumerable<Course> ListAll();
        Course Update(Course course);
        void Delete(Guid id);
    }
}
