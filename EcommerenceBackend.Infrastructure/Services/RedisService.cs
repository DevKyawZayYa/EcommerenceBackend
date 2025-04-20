using EcommerenceBackend.Application.Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EcommerenceBackend.Infrastructure.Services
{
    public class RedisService : IRedisService
    {
        private readonly StackExchange.Redis.IDatabase _db;

        public RedisService(IConfiguration config)
        {
            var redis = ConnectionMultiplexer.Connect(config["Redis:ConnectionString"]);
            _db = redis.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            return value.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(value);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var json = JsonSerializer.Serialize(value);
            await _db.StringSetAsync(key, json, expiry);
        }

        public async Task RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }
    }
}
