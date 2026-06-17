namespace System_uznawania_przychodow.Entities;

public class IndividualClient
{
    public int ClientId { get; set; }
    public string FirstName { get; set; } =  string.Empty;
    public string LastName { get; set; } =  string.Empty;
    public string Pesel { get; set; } =  string.Empty;

    public Client Client { get; set; } = null!;
}