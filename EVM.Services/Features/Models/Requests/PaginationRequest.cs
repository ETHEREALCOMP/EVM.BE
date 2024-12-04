using EVM.Data.Enums;

namespace EVM.Services.Features.Models.Requests;

public class PaginationRequest
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public SortType Type { get; set; }
}
