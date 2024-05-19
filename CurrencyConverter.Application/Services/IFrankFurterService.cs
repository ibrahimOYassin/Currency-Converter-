using CurrencyConverter.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Application.Services
{
    public interface IFrankFurterService
    {
        Task<ExchangeRate> GetLatestRatesAsync(string baseCurrency);
        Task<ExchangeRate> GetHistoricalRatesAsync(string baseCurrency, string startDate, string endDate);
    }
}
