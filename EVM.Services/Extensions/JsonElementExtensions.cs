using System.Text.Json;

namespace EVM.Services.Extensions;

public static class JsonElementExtensions
{
    public static bool TryGetString(this JsonElement source, out string? value)
    {
        value = source.GetString();

        return value is not null;
    }
}