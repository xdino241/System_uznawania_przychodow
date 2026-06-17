using System.ComponentModel.DataAnnotations;

namespace System_uznawania_przychodow.DTOs.Payments;

public class AddPaymentDto
{
    [Required]
    public int ContractId { get; set; }
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public DateOnly PaymentDate { get; set; }
}