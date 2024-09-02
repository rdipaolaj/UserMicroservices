using ssptb.pe.tdlt.user.entities.Entities;

namespace ssptb.pe.tdlt.user.redis.Users;
public interface IRedisUserService
{
    Task SyncUserWithRedisAsync(User user, string username);
}