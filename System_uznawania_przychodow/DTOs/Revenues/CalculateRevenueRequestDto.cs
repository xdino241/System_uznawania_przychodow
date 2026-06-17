using System.ComponentModel.DataAnnotations;

namespace System_uznawania_przychodow.DTOs.Revenues;

public class CalculateRevenueRequestDto
{
    public int? SoftwareId { get; set; }
    [MinLength(3)]
    [MaxLength(3)]
    public string CurrencyCode { get; set; } = string.Empty;
    public bool IsPredicted { get; set; }
}