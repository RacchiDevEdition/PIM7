using System;
using PIM.Domain.Entities;

namespace PIM.Models.Interfaces
{
    public interface IEnrollmentService
    {
        Enrollment Enroll(Guid studentId, Guid courseId);
        void Unenroll(Guid enrollmentId);
        Enrollment? GetById(Guid id);
    }
}
