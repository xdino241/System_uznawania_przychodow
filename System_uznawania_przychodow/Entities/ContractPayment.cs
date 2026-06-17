namespace System_uznawania_przychodow.Entities;

public class ContractPayment
{
    public int ContractPaymentId { get; set; }
    public int ContractId { get; set; }
    public decimal Amount { get; set; }
    public DateOnly PaymentDate { get; set; }

    public Contract Contract { get; set; } = null!;
}