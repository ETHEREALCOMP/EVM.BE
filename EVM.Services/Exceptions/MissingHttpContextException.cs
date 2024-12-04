namespace EVM.Services.Exceptions;

public class MissingHttpContextException()
    : BaseCustomException("Something went wrong.", "HttpContext is null")
{
}