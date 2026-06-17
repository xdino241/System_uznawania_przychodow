namespace System_uznawania_przychodow.DTOs.Revenues;

public class RevenueResponseDto
{
    public decimal TotalRevenue { get; set; }
    public string Currency { get; set; } = null!;
}