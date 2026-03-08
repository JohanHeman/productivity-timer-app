using ProductivityTimer.Domain.Interfaces;
using ProductivityTimer.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.UseCase.UseCases
{
    public class GetQuoteUseCase
    {
        private readonly IQuoteService _quoteService;

        public GetQuoteUseCase(IQuoteService quoteService)
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
