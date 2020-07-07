using System.Collections.Generic;
using Newtonsoft.Json;

namespace Investimentos.Application.Models
{
    public class TdsModel
    {
        [JsonProperty("tds")]
        public IEnumerable<TesouroDiretoModel> TesouroDiretos { get; set; }
    }
}