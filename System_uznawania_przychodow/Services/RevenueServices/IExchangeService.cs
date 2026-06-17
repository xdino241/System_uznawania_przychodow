namespace System_uznawania_przychodow.Services.RevenueServices;

public interface IExchangeService
{
    Task<decimal> GetExchangeRateAsync(string currencyCode);
}