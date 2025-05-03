using EcommerenceBackend.Application.Interfaces.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace EcommerenceBackend.Infrastructure.Services
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase? _db;
        private readonly ILogger<RedisService> _logger;

        public RedisService(IConfiguration config, ILogger<RedisService> logger)
        {
            _logger = logger;

            try
            {
                var redis = ConnectionMultiplexer.Connect(config["Redis:ConnectionString"]);
                _db = redis.GetDatabase();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to Redis. Redis caching will be skipped.");
                _db = null;
            }
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            if (_db == null) return default;

            try
            {
                var value = await _db.StringGetAsync(key);
                return value.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(value);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Redis GET failed for key: {key}");
                return default;
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            if (_db == null) return;

            try
            {
                var json = JsonSerializer.Serialize(value);
                await _db.StringSetAsync(key, json, expiry);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Redis SET failed for key: {key}");
            }
        }

        public async Task RemoveAsync(string key)
        {
            if (_db == null) return;

            try
            {
                await _db.KeyDeleteAsync(key);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Redis REMOVE failed for key: {key}");
            }
        }
    }
}
