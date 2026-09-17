using System;
using PIM.Domain.Entities;

namespace PIM.Models.Interfaces
{
    public interface IUserService
    {
        User Create(User user, string password);
        User? Authenticate(string email, string password);
        User? GetById(Guid id);
    }
}
