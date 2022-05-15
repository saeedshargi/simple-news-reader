using SimpleNewsReader.Domain.Exceptions;

namespace SimpleNewsReader.Application.Exceptions;

public class ValidationException: BusinessApplicationException
{
    public ValidationException( string message) : base("Validation Failure", message)
    {
    }
}