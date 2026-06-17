namespace System_uznawania_przychodow.Exceptions;

public class CompanyClientCantBeDeletedException : Exception
{
    public CompanyClientCantBeDeletedException(){}
    public CompanyClientCantBeDeletedException(string? message) : base(message){}
    public CompanyClientCantBeDeletedException(string? message, Exception? innerException) : base(message, innerException) { }
}