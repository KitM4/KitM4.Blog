namespace KitM4.Blog.Domain.Exceptions;

public class InvalidRequestDataException : Exception
{
    public InvalidRequestDataException(string message) : base(message) { }

    public InvalidRequestDataException(IEnumerable<string> errorMessages) : base(string.Join('\n', errorMessages)) { }
}