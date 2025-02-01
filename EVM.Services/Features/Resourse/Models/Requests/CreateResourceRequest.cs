using EVM.Data.Enums;
using EVM.Services.Features.Models.Responses;
using EVM.Services.Features.Resourse.Models.Responses;
using MediatR;

namespace EVM.Services.Features.Resourse.Models.Requests;

public record CreateResourceRequest : IRequest<ApiResponse<CreateResourcesResponse>>
{
    public required List<CreateResource> Resources { get; set; }

    public required Guid UserId { get; set; }

    public required Guid EventId { get; set; }
}
