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
    public class FundoService : IFundoService
    {
        private readonly HttpClient _client;
        private readonly ApplicationOptions _options;
        private readonly ILogger<FundoService> _logger;

        public FundoService(HttpClient client, IOptions<ApplicationOptions> options, ILogger<FundoService> logger)
        {
            _client = client;
            _options = options?.Value;
            _logger = logger;
        }

        public async Task<Result<FundosModel>> GetFundos()
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