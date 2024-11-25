using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ABCMoneyTransfer_Project.Models;


namespace ABCMoneyTransfer_Project.Services
{
    public class ExchangeRateService
    {

        // ----------------------------------------------------------------------------------------------------------------------------------

        private readonly HttpClient _httpClient;

        // ----------------------------------------------------------------------------------------------------------------------------------

        public ExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public async Task<decimal?> GetExchangeRatesAsync()
        {
            try
            {
                var apiUrl = "https://www.nrb.org.np/api/forex/v1/rates?page=1&per_page=5&from=2024-06-12&to=2024-06-12";
                var response = await _httpClient.GetStringAsync(apiUrl);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                var exchangeRates = JsonSerializer.Deserialize<ExchangeRateResponse>(response, options);

                if (exchangeRates?.Data?.Payload != null)
                {
                    var rate_change = exchangeRates.Data.Payload
                        .SelectMany(p => p.Rates)
                        .ToList();

                    var rate = rate_change.FirstOrDefault(r => r.Currency.Iso3 == "MYR");

                    if (rate != null)
                    {
                        return rate.Sell;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

    }
}
