using Peanut_SE_Task.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Peanut_SE_Task.Factories
{
    public class ExchangeServiceFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ExchangeServiceFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IExchangeService CreateBinanceService()
        {
            var client = _httpClientFactory.CreateClient("Binance");
            client.BaseAddress = new Uri("https://api.binance.com/");
            return new BinanceService(client);
        }

        public IExchangeService CreateKucoinService()
        {
            var client = _httpClientFactory.CreateClient("KuCoin");
            client.BaseAddress = new Uri("https://api.kucoin.com/");
            return new KucoinService(client);
        }
    }
}
