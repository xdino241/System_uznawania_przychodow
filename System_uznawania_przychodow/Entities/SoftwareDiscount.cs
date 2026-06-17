namespace System_uznawania_przychodow.Entities;

public class SoftwareDiscount
{
    public int SoftwareId { get; set; }
    public int DiscountId { get; set; }
    public Software Software { get; set; } = null!;
    public Discount Discount { get; set; } = null!;
}