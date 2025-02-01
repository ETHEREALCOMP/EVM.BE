using EVM.Data;
using EVM.Data.Enums;
using EVM.Services.Exceptions;
using EVM.Services.Extensions;
using EVM.Services.Features.Event.Commands;
using EVM.Services.Features.Event.Models.Requests;
using EVM.Services.Features.Event.Models.Responses;
using EVM.Services.Features.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Services.Features.Event.Query;
public class GetEventsQueryHandler
  (ILogger<GetEventsQueryHandler> _logger, AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor)
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();
    public async Task<ApiResponse<GetEventResponse>> Handle(CancellationToken cancellationToken)
    {
        var userId = _httpContext.User.GetId() ?? throw new UserNotFoundException();
        var events = await _appDbContext.Events.Where(x => x.UserId == userId).FirstOrDefaultAsync(cancellationToken);

        return new(new() {ETask = events.EventTasks.ToList(), Name = events.Title});
    }
}
