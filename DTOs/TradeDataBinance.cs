using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peanut_SE_Task.DTOs
{
    public class TradeDataBinance
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("qty")]
        public decimal Quantity { get; set; }

        [JsonProperty("quoteQty")]
        public decimal QuoteQuantity { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("isBuyerMaker")]
        public bool IsBuyerMaker { get; set; }

        [JsonProperty("isBestMatch")]
        public bool IsBestMatch { get; set; }
    }

}
