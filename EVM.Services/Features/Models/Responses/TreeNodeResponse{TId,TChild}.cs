namespace EVM.Services.Features.Models.Responses;

public class TreeNodeResponse<TId, TChild>
{
    public required TId Id { get; set; }

    public required string? Name { get; set; }

    public List<TChild> Children { get; set; } = [];
}
