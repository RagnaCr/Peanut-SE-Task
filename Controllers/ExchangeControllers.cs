using System;
using Microsoft.AspNetCore.Mvc;
using Peanut_SE_Task.Services;
using System.Threading.Tasks;

namespace Peanut_SE_Task.Controllers
{
    [ApiController]
    [Route("api/binance")]
    public class BinanceController : ControllerBase
    {
        private readonly BinanceService _binanceService;

        public BinanceController(BinanceService binanceService)
        {
            _binanceService = binanceService;
        }

        [HttpGet("last-trade-price")]
        public async Task<IActionResult> GetLastTradePrice([FromQuery] string baseCurrency, [FromQuery] string quoteCurrency)
        {
            if (string.IsNullOrEmpty(baseCurrency) || string.IsNullOrEmpty(quoteCurrency))
            {
                return BadRequest("Base currency and quote currency are required.");
            }

            try
            {
                var price = await _binanceService.GetLastTradePriceAsync(baseCurrency, quoteCurrency);
                return Ok(new { Price = price });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    [ApiController]
    [Route("api/kucoin")]
    public class KucoinController : ControllerBase
    {
        private readonly KucoinService _kucoinService;

        public KucoinController(KucoinService kucoinService)
        {
            _kucoinService = kucoinService;
        }

        [HttpGet("last-trade-price")]
        public async Task<IActionResult> GetLastTradePrice([FromQuery] string baseCurrency, [FromQuery] string quoteCurrency)
        {
            if (string.IsNullOrEmpty(baseCurrency) || string.IsNullOrEmpty(quoteCurrency))
            {
                return BadRequest("Base currency and quote currency are required.");
            }

            try
            {
                var price = await _kucoinService.GetLastTradePriceAsync(baseCurrency, quoteCurrency);
                return Ok(new { Price = price });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
