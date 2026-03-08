using Microsoft.Extensions.Logging;
using ProductivityTimer.Infrastructure.Services.APIModels;
using ProductivityTimer.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using ProductivityTimer.Domain.Interfaces;

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
            string uri = GetRandomCategory();

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

        private string GetRandomCategory()
        {
            int num = Random.Shared.Next(4);
            string category;
            switch (num)
            {
                case 0:
                    category = "inspirational";
                    break;
                case 1:
                    category = "life";
                    break;
                case 2:
                    category = "wisdom";
                    break;
                case 3:
                default:
                    category = "success";
                    break;
            }

            return $"v2/quotes?category={category}";
        }
    }
}
