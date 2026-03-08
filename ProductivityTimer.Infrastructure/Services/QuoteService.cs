using Microsoft.Extensions.Logging;
using ProductivityTimer.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using ProductivityTimer.Domain.Interfaces;
using ProductivityTimer.Infrastructure.APIModels;

namespace ProductivityTimer.Infrastructure.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly HttpClient _client;
        private readonly ILogger<QuoteService> _logger;
        public QuoteService(HttpClient client, ILogger<QuoteService> logger)
        {
            _client = client;
            _logger = logger;
        }
        public async Task<Quote> GetQuoteAsync()
        {
            string uri = "v2/randomquotes?categories=success,wisdom,life,inspirational";

            try
            {

                var response = await _client.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException("Failed to get quote from API");
                }

                var responseString = await response.Content.ReadAsStringAsync();
                var quotes = JsonSerializer.Deserialize<List<QuoteResponse>>(responseString);

                if (quotes != null && quotes.Count > 0)
                {
                    var quote = new Quote
                    {
                        Text = quotes[0].Quote,
                        Author = quotes[0].Author
                    };
                    return quote;
                }
                throw new InvalidOperationException("Failed to get quote from API");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get quote from API");
                throw;
            }
        }
    }
}
