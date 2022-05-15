using SimpleNewsReader.Domain.Exceptions;

namespace SimpleNewsReader.Application.Exceptions;

public class MappingException: BusinessApplicationException
{
    public MappingException(string message) : base("Mapping Failure", message)
    {
    }
}