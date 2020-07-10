using System;
using Investimentos.Application.Configuration;
using Investimentos.Application.Interfaces;
using Investimentos.Application.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Investimentos.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private MemoryCacheEntryOptions CacheEntryOptions 
            => new MemoryCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddDays(1).Date);

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public FundosModel GetFundos()
        {
            if(_cache.TryGetValue(CacheKeys.Fundos, out FundosModel obj))
                return obj;

            return default;
        }

        public void AddFundos(FundosModel model)
        {
            _cache.Set(CacheKeys.Fundos, model, CacheEntryOptions);
        }

        public TdsModel GetTesourosDiretos()
        {
            if(_cache.TryGetValue(CacheKeys.TesourosDiretos, out TdsModel obj))
                return obj;

            return default;
        }

        public void AddTesourosDiretos(TdsModel model)
        {
            _cache.Set(CacheKeys.TesourosDiretos, model, CacheEntryOptions);
        }

        public LcisModel GetRendasFixas()
        {
            if(_cache.TryGetValue(CacheKeys.RendasFixas, out LcisModel obj))
                return obj;

            return default;
        }

        public void AddRendasFixas(LcisModel model)
        {
            _cache.Set(CacheKeys.RendasFixas, model, CacheEntryOptions);
        }
    }
}