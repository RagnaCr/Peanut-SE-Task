using System;
using Peanut_SE_Task.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peanut_SE_Task.Services
{
    public class ExchangeEstimator
    {
        private readonly IEnumerable<IExchangeService> _exchangeServices;

        public ExchangeEstimator(IEnumerable<IExchangeService> exchangeServices)
        {
            _exchangeServices = exchangeServices;
        }

        public async Task<EstimateResponse> EstimateAsync(decimal inputAmount, string inputCurrency, string outputCurrency)
        {
            string bestExchange = null;
            decimal bestOutputAmount = 0;

            foreach (var service in _exchangeServices)
            {
                var rate = await service.GetLastTradePriceAsync(inputCurrency, outputCurrency);
                var outputAmount = inputAmount * rate;

                if (service.GetInverseState())
                {
                    outputAmount = inputAmount / rate;
                }

                if (outputAmount > bestOutputAmount)
                {
                    bestOutputAmount = outputAmount;
                    bestExchange = service.GetExchangeName();
                }
            }

            return new EstimateResponse
            {
                ExchangeName = bestExchange,
                OutputAmount = bestOutputAmount
            };
        }
    }

}
