using EVM.Data;
using EVM.Data.Models.TicketFeature;
using EVM.Services.Exceptions;
using EVM.Services.Extensions;
using EVM.Services.Features.Event.Models.Requests;
using EVM.Services.Features.Event.Models.Responses;
using EVM.Services.Features.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EVM.Services.Features.Event.Commands;

public class CreateEventCommandHandler
    (ILogger<CreateEventCommandHandler> _logger, AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor, IAuthorizationService _authorizationService)
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task<ApiResponse<CreateEventAndTicketResponse>> Handle(CreateEventRequest request, CancellationToken cancellationToken)
    {
        await _authorizationService.CanPerformActionAsync(_httpContext.User, "Create", "Event");

        var userId = _httpContext.User?.GetId() ?? throw new UserNotFoundException();
        var user = await _appDbContext.Users
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken)
            ?? throw new UserNotFoundException();

        if (user.Role != Data.Enums.UserRole.Organizer)
        {
            user.Role = Data.Enums.UserRole.Organizer;
        }

        Guid? ticketId = null;

        using var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var newEvent = new Data.Models.EventFeature.Event
            {
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
                Role = user.Role,
            };

            _appDbContext.Events.Add(newEvent);
            _logger.LogInformation("Event created successfully with ID: {EventId}", newEvent.Id);

            if (request.IsTicket)
            {
                var ticket = new Ticket
                {
                    Price = request.TicketRequest.Price,
                    Type = request.TicketRequest.Type ?? Data.Enums.TicketType.None,
                    EventId = newEvent.Id,
                    UserId = userId,
                };

                ticketId = ticket.Id;

                _appDbContext.Tickets.Add(ticket);
                await _appDbContext.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Ticket created successfully for Event ID: {EventId}", newEvent.Id);
            }

            await transaction.CommitAsync(cancellationToken);

            return new(new() { Id = newEvent.Id, TicketId = ticketId });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Failed to create event and ticket.");
            throw;
        }
    }
}


