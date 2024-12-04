using EVM.Data.Enums;
using System.Text.Json;

namespace EVM.Services.Features.Models.Requests;

public class FilterRequest
{
    public required FilterType Code { get; set; }

    public required JsonElement Value { get; set; }
}