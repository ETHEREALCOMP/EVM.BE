using EVM.Data;
using EVM.Data.Models.IdentityFeature;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EVM.Services.Service;

public class CreateUserService(AppDbContext _dbContext, UserManager<User> _userManager, ILogger<CreateUserService> _logger)
{
    public async Task CreateAsync(User newUser, string? password = null, CancellationToken cancellationToken = default)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var result = await (password is null ? _userManager.CreateAsync(newUser) : _userManager.CreateAsync(newUser, password));

            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
