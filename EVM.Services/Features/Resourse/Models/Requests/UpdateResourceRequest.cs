using EVM.Data.Enums;

namespace EVM.Services.Features.Resourse.Models.Requests;

public class UpdateResourceRequest
{
    public string? Name { get; set; }

    public ResourceType? Type { get; set; }
}
