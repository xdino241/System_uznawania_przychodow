namespace System_uznawania_przychodow.Exceptions;

public class WrongNumberFormatException : Exception
{
    public  WrongNumberFormatException() {}
    public WrongNumberFormatException(string? message) : base(message) {}
    public WrongNumberFormatException(string? message, Exception? innerException) : base(message, innerException) {}
}