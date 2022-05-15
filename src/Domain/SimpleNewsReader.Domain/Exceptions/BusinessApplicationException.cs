namespace SimpleNewsReader.Domain.Exceptions;

public class BusinessApplicationException: Exception
{
    public BusinessApplicationException(string title,string message): base(message) => Title = title;

    public string? Title { get; }
}