using EVM.Data;
using EVM.Data.Enums;
using EVM.Services.Exceptions;
using EVM.Services.Extensions;
using EVM.Services.Features.Event.Models.Requests;
using EVM.Services.Features.Models.Responses;
using EVM.Services.Service;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EVM.Services.Features.Event.Commands;

public class CreateEventCommandHandler
    (ILogger<CreateEventCommandHandler> _logger, AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor, CustomClaimsValidator _customClaimsValidator)
    : IRequestHandler<CreateEventRequest, ApiResponse<BaseResponse>>
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task<ApiResponse<BaseResponse>> Handle(CreateEventRequest request, CancellationToken cancellationToken)
    {
        await _customClaimsValidator.ValidateClaims();

        var userId = _httpContext.User?.GetId()
            ?? throw new UserNotFoundException();

        try
        {
            var user = await _appDbContext.Users
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new UserNotFoundException();

            var newEvent = new Data.Models.EventFeature.Event
            {
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
            };

            _appDbContext.Events.Add(newEvent);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Event created successfully with ID: {EventId}", newEvent.Id);
            return new(new() { Id = newEvent.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating event.");
            return new(HttpStatusCode.InternalServerError, "An error occurred while creating the event.");
        }
    }
}

