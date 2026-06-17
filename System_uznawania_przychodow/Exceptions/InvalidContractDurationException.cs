namespace System_uznawania_przychodow.Exceptions;

public class InvalidContractDurationException : Exception
{
    public InvalidContractDurationException()
    {
    }
    public InvalidContractDurationException(string? message) : base(message)
    {
    }

    public InvalidContractDurationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}