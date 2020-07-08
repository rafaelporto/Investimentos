using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Investimentos.Application.Configuration;
using Investimentos.Application.Interfaces;
using Investimentos.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Investimentos.Application.Services
{
    public class TesouroDiretoService : ITesouroDiretoService
    {
        private readonly ITesouroDiretoAdapter _adapter;
        private readonly HttpClient _client;
        private readonly ApplicationOptions _options;
        private readonly ILogger<TesouroDiretoService> _logger;

        public TesouroDiretoService(HttpClient client, 
                                    ITesouroDiretoAdapter adapter,
                                    IOptions<ApplicationOptions> options,
                                    ILogger<TesouroDiretoService> logger)
        {
            _adapter = adapter;
            _client = client;
            _options = options?.Value;
            _logger = logger;
        }

        public async Task<IEnumerable<InvestimentoModel>> GetInvestimentos()
        {
            var result = await GetTesourosDiretos();

            if (result.Succeeded)
            return _adapter.Map(result.Data.TesouroDiretos);
                
            return default;
        }

        private async Task<Result<TdsModel>> GetTesourosDiretos()
        {
            var url = _options.GetTesouroDireto();

            var httpResponse = await _client.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                var contentResult = await httpResponse.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<TdsModel>(contentResult);

                return Result<TdsModel>.Success(result);
            }

            return Result<TdsModel>.Failure("Não foi localizado os dados do tesouro direto.");
        }
    }
}