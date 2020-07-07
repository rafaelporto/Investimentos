using System.Collections.Generic;
using Newtonsoft.Json;

namespace Investimentos.Application.Models
{
    public class LcisModel
    {
        [JsonProperty("lcis")]
        public IEnumerable<RendaFixaModel> Lcis { get; set; }
    }
}