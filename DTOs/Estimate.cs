using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peanut_SE_Task.DTOs
{
    public class EstimateRequest
    {
        public decimal InputAmount { get; set; }
        public string InputCurrency { get; set; }
        public string OutputCurrency { get; set; }
    }

    public class EstimateResponse
    {
        public string ExchangeName { get; set; }
        public decimal OutputAmount { get; set; }
    }

}
