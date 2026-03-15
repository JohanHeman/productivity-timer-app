using ProductivityTimer.Domain.Interfaces;
using ProductivityTimer.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Application.Services
{
    public class QuoteApplicationService
    {
        private readonly IQuoteService _quoteService;

        public QuoteApplicationService(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        public async Task<Quote> GetQuoteAsync()
        {

            var quote = await _quoteService.GetQuoteAsync();
            return quote;
        }
    }
}
