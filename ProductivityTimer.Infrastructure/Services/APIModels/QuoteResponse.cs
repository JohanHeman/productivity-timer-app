using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProductivityTimer.Infrastructure.Services.APIModels
{
    public class QuoteResponse
    {
        [JsonPropertyName("quote")]
        public string  Quote { get; set; }
        [JsonPropertyName("author")]
        public string Author { get; set; }
    }
}
