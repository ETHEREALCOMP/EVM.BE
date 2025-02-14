using EVM.Data.Models.IdentityFeature;
using System;

namespace EVM.Services.Features.Event.Models.Responses;

public class GetTaskResponse
{
    public required Guid TaskId { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public required Data.Enums.TaskStatus Status { get; set; }
}
