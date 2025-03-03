namespace KitM4.Blog.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }

    public NotFoundException(object item, string key) : base($"The {item} {key} not found") { }
}