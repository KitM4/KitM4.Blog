namespace KitM4.Blog.Domain.Exceptions;

public class IncorrectCredentialsException : Exception
{
    public IncorrectCredentialsException() : base("Invalid password") { }

    public IncorrectCredentialsException(string message) : base(message) { }
}