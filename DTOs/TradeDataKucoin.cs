using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peanut_SE_Task.DTOs
{
    public class ApiResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("data")]
        public List<TradeDataKucoin> Data { get; set; }
    }

    public class TradeDataKucoin
    {
        [JsonProperty("sequence")]
        public string Sequence { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }
    }
}
