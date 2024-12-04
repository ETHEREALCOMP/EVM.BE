using EVM.Services.Exceptions;
using Microsoft.Extensions.Configuration;

namespace EVM.Services.Extensions;

public static class ConfigurationExtensions
{
    /// <summary>
    /// Gets a configuration value with the specified key.
    /// </summary>
    /// <param name="configurationSection">The configuration to enumerate.</param>
    /// <param name="key">The key of the configuration value.</param>
    /// <returns>The <see cref="String"/>.</returns>
    /// <remarks>
    ///     If no matching value is found with the specified key, an exception is raised.
    /// </remarks>
    /// <exception cref="RequiredConfigurationException">There is no value with key <paramref name="key"/>.</exception>
    public static string GetRequiredValue(this IConfigurationSection configurationSection, string key)
    {
        var value = configurationSection.GetValue<string>(key);

        if (String.IsNullOrWhiteSpace(value))
        {
            throw new RequiredConfigurationException(key);
        }

        return value;
    }
}
