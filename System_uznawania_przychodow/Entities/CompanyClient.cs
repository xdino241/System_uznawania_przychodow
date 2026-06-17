namespace System_uznawania_przychodow.Entities;

public class CompanyClient
{
    public int ClientId { get; set; }
    public string CompanyName { get; set; } =  string.Empty;
    public string Krs { get; set; } =  string.Empty;

    public Client Client { get; set; } = null!;
}