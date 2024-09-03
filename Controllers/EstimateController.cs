using Microsoft.AspNetCore.Mvc;
using Peanut_SE_Task.DTOs;
using Peanut_SE_Task.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peanut_SE_Task.Controllers
{
    [ApiController]
    [Route("api/")]
    public class EstimateController : ControllerBase
    {
        private readonly BinanceService _binanceService;
        private readonly KucoinService _kucoinService;

        public EstimateController(BinanceService binanceService, KucoinService kucoinService)
        {
            _binanceService = binanceService;
            _kucoinService = kucoinService;
        }

        
        [HttpGet("estimate")]
        public async Task<IActionResult> Estimate([FromQuery] EstimateRequest request)
        {
            if (request.InputAmount <= 0 || string.IsNullOrEmpty(request.InputCurrency) || string.IsNullOrEmpty(request.OutputCurrency))
            {
                return BadRequest("Invalid input parameters.");
            }
            try
            {
                ExchangeEstimator exchangeEstimator = new ExchangeEstimator(new List<IExchangeService> { _binanceService, _kucoinService });

                var estimateResponse = await exchangeEstimator.EstimateAsync(request.InputAmount, request.InputCurrency, request.OutputCurrency);

                return Ok(estimateResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    [ApiController]
    [Route("api")]
    public class StartPageController : ControllerBase
    {
        [HttpGet]
        public IActionResult Start()
        {
            return Ok(new { Start = "page" });
        }
    }

}
