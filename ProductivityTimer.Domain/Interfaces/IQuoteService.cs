using System;
using System.Collections.Generic;
using System.Text;
using ProductivityTimer.Domain.Entities;

namespace ProductivityTimer.Domain.Interfaces
{
    public interface IQuoteService
    {
        Task<Quote> GetQuoteAsync();
    }
}
