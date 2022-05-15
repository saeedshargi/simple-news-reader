namespace SimpleNewsReader.Domain.Exceptions;

public class BadRequestException: BusinessApplicationException
{
    public BadRequestException(string message) : base("Bas Request", message)
    {
    }
}