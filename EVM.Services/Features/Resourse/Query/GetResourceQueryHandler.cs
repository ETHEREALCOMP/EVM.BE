﻿using EVM.Data;
using EVM.Data.Models.ResourceFeature;
using EVM.Services.Exceptions;
using EVM.Services.Extensions;
using EVM.Services.Features.Models.Responses;
using EVM.Services.Features.Resourse.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EVM.Services.Features.Resourse.Query;

public class GetResourceQueryHandler
(ILogger<GetResourceQueryHandler> _logger, AppDbContext _appDbContext, IHttpContextAccessor _httpContextAccessor, IAuthorizationService _authorizationService)
{
    private readonly HttpContext _httpContext = _httpContextAccessor.HttpContext ?? throw new MissingHttpContextException();

    public async Task<ApiResponse<List<GetResourceResponse>>> Handle(CancellationToken cancellationToken)
    {
        await _authorizationService.CanPerformActionAsync(_httpContext.User, "Read", "Resource");

        var userId = _httpContext.User.GetId() ?? throw new UserNotFoundException();

        var resourcesQuery = await _appDbContext.Resources
            .Select(x => new GetResourceResponse
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type,
                Resources = x.EventResources.ToList(),
            })
            .ToListAsync(cancellationToken) ?? throw new EntityNotFoundException("Resources");

        return new(resourcesQuery);
    }
}