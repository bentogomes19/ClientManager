namespace ClientManager.src.Exceptions;

public class RequestValidationException : Exception
{
    public RequestValidationException() : base() { }
    public RequestValidationException(string message) : base(message) { }
    public RequestValidationException(string message, Exception exception) : base(message, exception) { }
}