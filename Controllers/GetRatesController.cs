using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Peanut_SE_Task.DTOs;
using Peanut_SE_Task.Services;

namespace Peanut_SE_Task.Controllers
{
    [ApiController]
    [Route("api/")]
    public class GetRatesController : ControllerBase
    {
        private readonly IEnumerable<IExchangeService> _exchangeServices;

        public GetRatesController(IEnumerable<IExchangeService> exchangeServices)
        {
            _exchangeServices = exchangeServices;
        }


        [HttpGet("getRates")]
        public async Task<IActionResult> GetRates([FromQuery] string baseCurrency, [FromQuery] string quoteCurrency)
        {
            if (string.IsNullOrEmpty(baseCurrency) || string.IsNullOrEmpty(quoteCurrency))
            {
                return BadRequest("Base currency and quote currency are required.");
            }

            var rates = new List<ExchangeRateResponse>();

            foreach (var service in _exchangeServices)
            {
                try
                {
                    var rate = await service.GetLastTradePriceAsync(baseCurrency, quoteCurrency);
                    if (service.GetInverseState())
                    {
                        rate = 1 / rate;
                    }
                    rates.Add(new ExchangeRateResponse
                    {
                        ExchangeName = service.GetExchangeName(),
                        Rate = rate
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            return Ok(rates);
        }
    }

}
