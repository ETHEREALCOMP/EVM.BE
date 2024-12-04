using System.Net;

namespace EVM.Services.Exceptions;

public class EntityNotFoundException(string _entityName) : BaseCustomException(_entityName + " is not found", HttpStatusCode.NotFound)
{
}