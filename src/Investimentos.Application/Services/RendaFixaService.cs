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
    public class RendaFixaService : IRendaFixaService
    {
        private readonly IRendaFixaAdapter _adapter;
        private readonly HttpClient _client;
        private readonly ICacheService _cacheService;
        private readonly ApplicationOptions _options;
        private readonly ILogger<RendaFixaService> _logger;

        public RendaFixaService(HttpClient client, 
                                IRendaFixaAdapter adapter,
                                ICacheService cacheService,
                                IOptions<ApplicationOptions> options,
                                ILogger<RendaFixaService> logger)
        {
            _adapter = adapter;
            _client = client;
            _cacheService = cacheService;
            _options = options?.Value;
            _logger = logger;
        }

        public async Task<IEnumerable<InvestimentoModel>> GetInvestimentos()
        {
            var modelCached = _cacheService.GetRendasFixas();

            if (modelCached != null)
                return _adapter.Map(modelCached.Lcis);

            var result = await GetRendasFixas();

            if (result.Succeeded)
            {
                _cacheService.AddRendasFixas(result.Data);
                return _adapter.Map(result.Data.Lcis);
            }

            return default;
        }

        private async Task<Result<LcisModel>> GetRendasFixas()
        {
            var url = _options.GetRendaFixa();

            var httpResponse = await _client.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                var contentResult = await httpResponse.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<LcisModel>(contentResult);

                return Result<LcisModel>.Success(result);
            }

            return Result<LcisModel>.Failure("Não foi localizado os dados de renda fixa.");
        }
    }
}