using PIM.Domain.Entities;

namespace PIM.Models.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
