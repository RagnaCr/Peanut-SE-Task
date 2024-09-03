using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peanut_SE_Task.Services
{
    public interface IExchangeService
    {
        Task<decimal> GetLastTradePriceAsync(string baseCurrency, string quoteCurrency);
        string GetExchangeName();
        bool GetInverseState();
    }
}
