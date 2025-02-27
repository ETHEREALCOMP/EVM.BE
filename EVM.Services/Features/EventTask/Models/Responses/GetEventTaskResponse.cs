using EVM.Data.Models.IdentityFeature;
using System;

namespace EVM.Services.Features.EventTask.Models.Responses;

public class GetEventTaskResponse
{
    public required Guid TaskId { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public required Data.Enums.TaskStatus Status { get; set; }
}
