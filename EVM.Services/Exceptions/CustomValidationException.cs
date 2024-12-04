using FluentValidation.Results;

namespace EVM.Services.Exceptions;

public class CustomValidationException(ValidationResult _result) : BaseCustomException(String.Join("; ", _result.Errors))
{
}
