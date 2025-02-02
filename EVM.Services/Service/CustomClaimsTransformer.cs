using EVM.Data.Models.IdentityFeature;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Services.Service;
public class CustomClaimsTransformer(ClaimsService _claimsService, UserManager<User> _userManager) : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var claims = await _claimsService.GenerateUserClaimsAsync(user);
                var identity = new ClaimsIdentity(claims);
                principal.AddIdentity(identity);
            }
        }

        return principal;
    }
}
