using EVM.Data;
using EVM.Data.Models.IdentityFeature;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EVM.Services.Service;

public class CreateUserService(UserManager<User> _userManager, ILogger<CreateUserService> _logger)
{
    public async Task CreateAsync(User newUser, string password, CancellationToken cancellationToken = default)
    {
        var result = await _userManager.CreateAsync(newUser, password);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Failed to create user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}