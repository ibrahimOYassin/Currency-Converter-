using CurrencyConverter.Application.Models;
using RestSharp;
using System.Threading.Tasks;

namespace CurrencyConverter.Application.Services
{
    public class FrankfurterService : IFrankFurterService
    {
        private readonly RestClient _client;

        public FrankfurterService()
        {
            _client = new RestClient("https://api.frankfurter.app/");
        }

        public async Task<ExchangeRate> GetLatestRatesAsync(string baseCurrency)
        {
            var request = new RestRequest("latest", Method.Get);
            request.AddParameter("from", baseCurrency);

            var response = await _client.ExecuteAsync<ExchangeRate>(request);
            if (response.IsSuccessful)
                return response.Data;

            // Retry logic if the first request fails
            for (int i = 0; i < 2; i++)
            {
                response = await _client.ExecuteAsync<ExchangeRate>(request);
                if (response.IsSuccessful)
                    return response.Data;
            }

            throw new Exception("Unable to fetch data from Frankfurter API");
        }

        public async Task<ExchangeRate> GetHistoricalRatesAsync(string baseCurrency, string startDate, string endDate)
        {
            var request = new RestRequest($"/{startDate}..{endDate}", Method.Get);
            request.AddParameter("from", baseCurrency);

            var response = await _client.ExecuteAsync<ExchangeRate>(request);
            if (response.IsSuccessful)
                return response.Data;

            // Retry logic if the first request fails
            for (int i = 0; i < 2; i++)
            {
                response = await _client.ExecuteAsync<ExchangeRate>(request);
                if (response.IsSuccessful)
                    return response.Data;
            }

            throw new Exception("Unable to fetch data from Frankfurter API");
        }

        
    }
}
