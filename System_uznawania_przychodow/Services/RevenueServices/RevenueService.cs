using Microsoft.EntityFrameworkCore;
using System_uznawania_przychodow.Data;
using System_uznawania_przychodow.DTOs.Revenues;
using System_uznawania_przychodow.Exceptions;

namespace System_uznawania_przychodow.Services.RevenueServices;

public class RevenueService : IRevenueService
{
    private readonly AppDbContext _dbContext;
    private readonly IExchangeService _exchangeService;

    public RevenueService(AppDbContext dbContext, IExchangeService exchangeService)
    {
        _dbContext = dbContext;
        _exchangeService =  exchangeService;
    }
    
    public async Task<RevenueResponseDto> CalculateRevenueAsync(CalculateRevenueRequestDto dto)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        decimal totalInPln;
        if (dto.IsPredicted)
        {
            totalInPln = await _dbContext.Contracts
                .Where(c => !dto.SoftwareId.HasValue || c.SoftwareId == dto.SoftwareId.Value)
                .Where(c => c.IsSigned || c.EndDate >= today)
                .SumAsync(c => c.Price);
        }
        else
        {
            totalInPln = await _dbContext.Contracts
                .Where(c => !dto.SoftwareId.HasValue || c.SoftwareId == dto.SoftwareId.Value)
                .Where(c => c.IsSigned)
                .SumAsync(c => c.Price);
        }

        decimal finalAmount = totalInPln;
        if (!string.Equals(dto.CurrencyCode, "PLN", StringComparison.OrdinalIgnoreCase))
        {
            decimal rate = await _exchangeService.GetExchangeRateAsync(dto.CurrencyCode);
            if (rate <= 0)
            {
                throw new UnsupportedCurrencyException();
            }
            finalAmount = totalInPln / rate;
        }

        return new RevenueResponseDto
        {
            TotalRevenue = Math.Round(finalAmount, 2),
            Currency = dto.CurrencyCode.ToUpper()
        };
    }
}