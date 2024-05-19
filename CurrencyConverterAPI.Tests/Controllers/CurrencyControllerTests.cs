using CurrencyConverter.Application.Models;
using CurrencyConverter.Application.Services;
using CurrencyConverter.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyConverterAPI.Tests.Controllers
{
    public class CurrencyControllerTests
    {
        private readonly Mock<IFrankFurterService> _mockService;
        private readonly CurrencyController _controller;

        public CurrencyControllerTests()
        {
            _mockService = new Mock<IFrankFurterService>();
            _controller = new CurrencyController(_mockService.Object);
        }

        [Fact]
        public async Task GetLatestRates_ReturnsOkResult()
        {
            // Arrange
            var baseCurrency = "EUR";
            _mockService.Setup(s => s.GetLatestRatesAsync(baseCurrency))
                        .ReturnsAsync(new ExchangeRate { Base = baseCurrency });

            // Act
            var result = await _controller.GetLatestRates(baseCurrency);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ExchangeRate>(okResult.Value);
            Assert.Equal(baseCurrency, returnValue.Base);
        }

        [Fact]
        public async Task ConvertCurrency_ReturnsBadRequest_ForExcludedCurrencies()
        {
            // Arrange
            var request = new ConversionRequest { Amount = 100, FromCurrency = "TRY", ToCurrency = "USD" };

            // Act
            var result = await _controller.ConvertCurrency(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Currency not supported for conversion", badRequestResult.Value);
        }

        [Fact]
        public async Task GetHistoricalRates_ReturnsOkResult()
        {
            // Arrange
            var baseCurrency = "EUR";
            var startDate = "2020-01-01";
            var endDate = "2020-01-31";
            _mockService.Setup(s => s.GetHistoricalRatesAsync(baseCurrency, startDate, endDate))
                        .ReturnsAsync(new ExchangeRate { Base = baseCurrency });

            // Act
            var result = await _controller.GetHistoricalRates(baseCurrency, startDate, endDate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ExchangeRate>(okResult.Value);
            Assert.Equal(baseCurrency, returnValue.Base);
        }
    }
}
