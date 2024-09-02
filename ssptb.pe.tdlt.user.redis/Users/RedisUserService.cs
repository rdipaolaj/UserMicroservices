using Microsoft.Extensions.Logging;
using ssptb.pe.tdlt.user.entities.Entities;
using ssptb.pe.tdlt.user.redis.Services;

namespace ssptb.pe.tdlt.user.redis.Users;
public class RedisUserService : IRedisUserService
{
    private readonly IRedisService _redisService;
    private readonly ILogger<RedisUserService> _logger;

    public RedisUserService(IRedisService redisService, ILogger<RedisUserService> logger)
    {
        _redisService = redisService;
        _logger = logger;
    }

    public async Task SyncUserWithRedisAsync(User user, string username)
    {
        var redisKey = $"user:{username}";
        var userInRedis = await _redisService.GetInformationAsync<User>(redisKey);
        if (userInRedis != null)
        {
            _logger.LogWarning("Usuario encontrado en Redis pero no en PostgreSQL. Eliminando de Redis para mantener la consistencia.");
            await _redisService.DeleteInformationAsync(redisKey);
        }

        var redisExpiration = TimeSpan.FromHours(1);
        await _redisService.SaveInformationAsJsonAsync(redisKey, user, redisExpiration);
    }
}