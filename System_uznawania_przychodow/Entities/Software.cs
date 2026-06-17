namespace System_uznawania_przychodow.Entities;

public class Software
{
    public int SoftwareId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CurrentVersion { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public ICollection<SoftwareDiscount> SoftwareDiscounts { get; set; } = [];
    public ICollection<Contract> Contracts { get; set; } = [];
}