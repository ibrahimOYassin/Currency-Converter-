

# CurrencyConverterSolution

This solution consists of two projects:
1. **CurrencyConverterAPI** - The ASP.NET Core Web API project for currency conversion.
2. **CurrencyConverterAPI.Tests** - The XUnit test project for unit testing the API.

## Table of Contents

- [Requirements](#requirements)
- [Setup and Running the Application](#setup-and-running-the-application)
  - [Clone the Repository](#1-clone-the-repository)
  - [Restore Dependencies](#2-restore-dependencies)
  - [Run the API](#3-run-the-api)
  - [Run the Tests](#4-run-the-tests)
- [API Endpoints](#api-endpoints)
  - [Retrieve the Latest Exchange Rates](#1-retrieve-the-latest-exchange-rates)
  - [Convert Amounts Between Different Currencies](#2-convert-amounts-between-different-currencies)
  - [Retrieve Historical Exchange Rates](#3-retrieve-historical-exchange-rates)
- [Assumptions](#assumptions)
- [Enhancements](#enhancements)
- [Contact](#contact)

## Requirements

- .NET 8 SDK
- A code editor like Visual Studio or Visual Studio Code

## Setup and Running the Application

### 1. Clone the Repository

```bash
git clone <repository-url>
cd CurrencyConverterSolution
```

### 1. Restore Dependencies

```bash
dotnet restore
```


### 3. Run the API

```bash
cd CurrencyConverterAPI
dotnet run
```


The API should now be running at https://localhost:7269 (or http://localhost:58324).


### 4. Run the Tests

```bash
cd ../CurrencyConverterAPI.Tests
dotnet test
```


## API Endpoints

###1. Retrieve the Latest Exchange Rates

**Endpoint: GET /Currency/latest/{baseCurrency}**

**Example: GET /Currency/latest/EUR**

Response:

```json
 {
  "base": "EUR",
  "rates": {
    "USD": 1.1,
    "GBP": 0.9
    // ... other currencies
  }
}
```

### 2. Convert Amounts Between Different Currencies
**Endpoint: POST /Currency/convert**

Request Body:

```json
{
  "amount": 100,
  "fromCurrency": "EUR",
  "toCurrency": "USD"
}
```

Response:

```json
 {
  "amount": 110
}
```

**Note: Conversions involving TRY, PLN, THB, and MXN are not supported and will return a 400 Bad Request response.**

### 3. Retrieve Historical Exchange Rates
**Endpoint: GET /Currency/historical/{baseCurrency}**

Query Parameters:

 - startDate: Start date for the historical data (format: YYYY-MM-DD)
 - endDate: End date for the historical data (format: YYYY-MM-DD)
 - page: Page number for pagination (default: 1)
 -  pageSize: Number of  items per page (default: 10)

Example: **`GET /Currency/historical/EUR?startDate=2020-01-01&endDate=2020-01-31&page=1&pageSize=10`**

Response:

```json{
  "rates": {
    "2020-01-01": {
      "USD": 1.1,
      "GBP": 0.9
      // ... other currencies
    },
    // ... other dates
  },
  "page": 1,
  "pageSize": 10
}
```


## Assumptions:
#### The Frankfurter API might not respond on the first attempt. We have implemented retry logic to handle this.
#### Only the specific currencies TRY, PLN, THB, and MXN are excluded from conversion in the second endpoint.
#### Basic pagination has been implemented for historical rates.


### Enhancements
Given more time, we could consider the following enhancements:

 1. **Improved Error Handling:** Add more granular error handling and custom error messages.
 2. **Caching:** Implement caching for exchange rates to reduce the number of requests to the Frankfurter API.
 3. **Rate Limiting:** Implement rate limiting to prevent overloading the Frankfurter API.
 4. **CI/CD Pipeline:** Set up a CI/CD pipeline for automated testing and deployment.
 5. **Swagger Documentation:** Add Swagger for better API documentation and testing.
 6. **Logging:** Implement comprehensive logging for better diagnostics and monitoring.
 7. **Docker Support:** Add Docker support for containerized deployment.
 8. **Authentication and Authorization:** Implement security features to restrict access to the API.
