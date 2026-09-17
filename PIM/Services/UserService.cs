using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PIM.Domain.Entities;
using PIM.Infrastructure;
using PIM.Models.Interfaces;

namespace PIM.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly PasswordHasher<User> _hasher;

        public UserService(AppDbContext db)
        {
            _db = db;
            _hasher = new PasswordHasher<User>();
        }

        public User Create(User user, string password)
        {
            user.PasswordHash = _hasher.HashPassword(user, password);
            switch (user)
            {
                case Student s:
                    _db.Students.Add(s);
                    break;
                case Teacher t:
                    _db.Teachers.Add(t);
                    break;
                default:
                    // fallback: add to Students set
                    _db.Students.Add((Student)user);
                    break;
            }

            _db.SaveChanges();
            return user;
        }

        public User? Authenticate(string email, string password)
        {
            var user = _db.Set<User>().FirstOrDefaultAsync(u => u.Email == email).GetAwaiter().GetResult();
            if (user == null) return null;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Success)
                return user;

            return null;
        }

        public User? GetById(Guid id)
        {
            return _db.Set<User>().Find(id);
        }
    }
}
