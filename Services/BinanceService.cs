using Newtonsoft.Json;
using Peanut_SE_Task.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace Peanut_SE_Task.Services
{
    public class BinanceService : IExchangeService
    {
        private readonly HttpClient _httpClient;
        private bool inverse = false;

        public BinanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetLastTradePriceAsync(string baseCurrency, string quoteCurrency)
        {
            var response = await _httpClient.GetAsync($"api/v3/trades?symbol={baseCurrency}{quoteCurrency}");
            if (!response.IsSuccessStatusCode)
            {
                response = await _httpClient.GetAsync($"api/v3/trades?symbol={quoteCurrency}{baseCurrency}");
                inverse = true;
            }
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var priceData = JsonConvert.DeserializeObject<List<TradeDataBinance>>(content);
            if (priceData == null)
            {
                throw new Exception("No trade data available.");
            }
            return priceData[0].Price;
        }

        public string GetExchangeName()
        {
            return "Binance";
        }
        public bool GetInverseState()
        {
            return inverse;
        }
    }

}
