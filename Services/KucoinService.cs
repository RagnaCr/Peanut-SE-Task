using System;
using Newtonsoft.Json;
using Peanut_SE_Task.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace Peanut_SE_Task.Services
{
    public class KucoinService : IExchangeService
    {
        private readonly HttpClient _httpClient;
        private bool inverse = false;
        public KucoinService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetLastTradePriceAsync(string baseCurrency, string quoteCurrency)
        {
            var response = await _httpClient.GetAsync($"/api/v1/market/histories?symbol={baseCurrency}-{quoteCurrency}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);

            if (apiResponse.Data == null || apiResponse.Data.Count == 0)
            {
                response = await _httpClient.GetAsync($"/api/v1/market/histories?symbol={quoteCurrency}-{baseCurrency}");
                inverse = true;
            }
            response.EnsureSuccessStatusCode();
            content = await response.Content.ReadAsStringAsync();
            apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);

            if (apiResponse.Data == null || apiResponse.Data.Count == 0)
            {
                throw new Exception("No trade data available.");
            }

            TradeDataKucoin firstTrade = apiResponse.Data.First();
            return firstTrade.Price;
        }

        public string GetExchangeName()
        {
            return "Kucoin";
        }

        public bool GetInverseState()
        {
            return inverse;
        }
    }
}
