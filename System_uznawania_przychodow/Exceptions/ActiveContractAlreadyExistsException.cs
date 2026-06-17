namespace System_uznawania_przychodow.Exceptions;

public class ActiveContractAlreadyExistsException : Exception
{
    public ActiveContractAlreadyExistsException()
    {
    }

    public ActiveContractAlreadyExistsException(string? message) : base(message)
    {
    }

    public ActiveContractAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
    
}