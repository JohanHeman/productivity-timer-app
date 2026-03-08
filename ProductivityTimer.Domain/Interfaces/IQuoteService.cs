using System;
using System.Collections.Generic;
using System.Text;
using ProductivityTimer.Domain.Models.Entities;

namespace ProductivityTimer.Domain.Interfaces
{
    public interface IQuoteService
    {
        Task<Quote> GetQuoteAsync();
    }
}
