namespace EVM.Services.Exceptions;

public class RequiredConfigurationException(string configurationName) : Exception($"Required configuration for {configurationName} not found")
{
}
