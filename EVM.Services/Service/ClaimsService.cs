using EVM.Data;
using EVM.Data.Models.IdentityFeature;
using System.Security.Claims;

namespace EVM.Services.Service;

public class ClaimsService(AppDbContext _dbContext)
{
    public async Task<List<Claim>> GenerateUserClaimsAsync(User user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name),
        };

        return claims;
    }
}
