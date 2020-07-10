using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Investimentos.Application.Configuration;
using Investimentos.Application.Interfaces;
using Investimentos.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Investimentos.Application.Services
{
    public class
    FundoService : IFundoService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _client;
        private readonly ICacheService _cacheService;
        private readonly ApplicationOptions _options;
        private readonly ILogger<FundoService> _logger;

        public FundoService(HttpClient client,
                            IMapper mapper,
                            ICacheService cacheService,
                            IOptions<ApplicationOptions> options,
                            ILogger<FundoService> logger)
        {
            _mapper = mapper;
            _client = client;
            _cacheService = cacheService;
            _options = options?.Value;
            _logger = logger;
        }

        public async Task<IEnumerable<InvestimentoModel>> GetInvestimentos()
        {
            var modelsCached = _cacheService.GetFundos();

            if (modelsCached != null)
                return _mapper.Map<IEnumerable<InvestimentoModel>>(modelsCached.Fundos);

            var result = await GetFundos();

            if (result.Succeeded)
            {
                _cacheService.AddFundos(result.Data);
                return _mapper.Map<IEnumerable<InvestimentoModel>>(result.Data.Fundos);
            }

            return new List<InvestimentoModel>();
        }

        private async Task<Result<FundosModel>> GetFundos()
        {
            var url = _options.GetFundos();

            var httpResponse = await _client.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                var contentResult = await httpResponse.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<FundosModel>(contentResult);

                return Result<FundosModel>.Success(result);
            }

            return Result<FundosModel>.Failure("Não foi localizado os dados dos fundos.");
        }
    }
}