using System_uznawania_przychodow.DTOs.Revenues;

namespace System_uznawania_przychodow.Services.RevenueServices;

public interface IRevenueService
{
    Task<RevenueResponseDto> CalculateRevenueAsync(CalculateRevenueRequestDto dto);
}