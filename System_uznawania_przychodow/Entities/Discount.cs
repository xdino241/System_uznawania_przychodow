namespace System_uznawania_przychodow.Entities;

public class Discount
{
    public int DiscountId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public ICollection<SoftwareDiscount> SoftwareDiscounts { get; set; } = [];
}