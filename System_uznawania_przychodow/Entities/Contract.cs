namespace System_uznawania_przychodow.Entities;

public class Contract
{
    public int ContractId { get; set; }
    public int ClientId { get; set; }
    public int SoftwareId { get; set; }
    public string SoftwareVersion { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int SupportYears { get; set; }
    public decimal Price { get; set; }
    public bool IsSigned { get; set; } = false;

    public Client Client { get; set; } = null!;
    public Software Software { get; set; } = null!;
    public ICollection<ContractPayment> Payments { get; set; } = [];
}