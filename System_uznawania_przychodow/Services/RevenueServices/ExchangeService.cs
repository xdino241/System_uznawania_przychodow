using System.Text.Json.Nodes;

namespace System_uznawania_przychodow.Services.RevenueServices;

public class ExchangeService : IExchangeService
{
    private readonly HttpClient _httpClient;
    
    public ExchangeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetExchangeRateAsync(string currencyCode)
    {
        try
        {
            string url = $"https://api.nbp.pl/api/exchangerates/rates/A/{currencyCode}/?format=json";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return 0m;
            }
            var jsonString = await response.Content.ReadAsStringAsync();
            var jsonNode = JsonNode.Parse(jsonString);
            decimal rate = (decimal)jsonNode["rates"][0]["mid"];
            return rate;
        }
        catch (Exception)
        {
            return 0m;
        }
    }
}