namespace System_uznawania_przychodow.Entities;

public class Client
{
    public int ClientId { get; set; }
    public string Address { get; set; } =  string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Phone {get; set;} = string.Empty;
    
    public bool IsDeleted { get; set; } = false;

    public CompanyClient? CompanyClient { get; set; }
    public IndividualClient? IndividualClient { get; set; }
    public ICollection<Contract> Contracts { get; set; } = [];
}