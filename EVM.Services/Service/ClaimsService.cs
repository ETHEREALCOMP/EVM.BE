using EVM.Data;
using EVM.Data.Models.IdentityFeature;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EVM.Services.Service;

public class ClaimsService()
{
    public async Task<List<Claim>> GenerateUserClaimsAsync(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("username", user.UserName),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        };

        return claims;
    }
}
