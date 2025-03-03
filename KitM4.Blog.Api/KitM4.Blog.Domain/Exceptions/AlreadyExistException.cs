namespace KitM4.Blog.Domain.Exceptions;

public class AlreadyExistException : Exception
{
    public AlreadyExistException(string message) : base(message) { }

    public AlreadyExistException(object item, string key) : base($"The {item} {key} already exist") { }
}