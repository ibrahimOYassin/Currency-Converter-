using CurrencyConverter.Application.Models;
using CurrencyConverter.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly IFrankFurterService _frankfurterService;
        private static readonly string[] ExcludedCurrencies = { "TRY", "PLN", "THB", "MXN" };

        public CurrencyController(IFrankFurterService frankfurterService)
        {
            _frankfurterService = frankfurterService;
        }

        [HttpGet("latest/{baseCurrency}")]
        public async Task<IActionResult> GetLatestRates(string baseCurrency)
        {
            try
            {
                var rates = await _frankfurterService.GetLatestRatesAsync(baseCurrency);
                return Ok(rates);
            }
            catch
            {
                return StatusCode(503, "Service Unavailable");
            }
        }

        [HttpPost("convert")]
        public async Task<IActionResult> ConvertCurrency([FromBody] ConversionRequest request)
        {
            if (ExcludedCurrencies.Contains(request.FromCurrency) || ExcludedCurrencies.Contains(request.ToCurrency))
                return BadRequest("Currency not supported for conversion");

            try
            {
                var rates = await _frankfurterService.GetLatestRatesAsync(request.FromCurrency);
                if (rates.Rates.TryGetValue(request.ToCurrency, out decimal rate))
                {
                    var convertedAmount = request.Amount * rate;
                    return Ok(new { Amount = convertedAmount });
                }
                else
                {
                    return BadRequest("Conversion rate not found");
                }
            }
            catch
            {
                return StatusCode(503, "Service Unavailable");
            }
        }

        [HttpGet("historical/{baseCurrency}")]
        public async Task<IActionResult> GetHistoricalRates(string baseCurrency, [FromQuery] string startDate, [FromQuery] string endDate, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var rates = await _frankfurterService.GetHistoricalRatesAsync(baseCurrency, startDate, endDate);
                var paginatedRates = rates.Rates.Skip((page - 1) * pageSize).Take(pageSize).ToDictionary(kv => kv.Key, kv => kv.Value);
                return Ok(new { Rates = paginatedRates, Page = page, PageSize = pageSize });
            }
            catch
            {
                return StatusCode(503, "Service Unavailable");
            }
        }
    }
}
