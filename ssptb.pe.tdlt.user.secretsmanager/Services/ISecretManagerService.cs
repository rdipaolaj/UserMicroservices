using ssptb.pe.tdlt.user.common.Secrets;

namespace ssptb.pe.tdlt.user.secretsmanager.Services;
public interface ISecretManagerService
{
    Task<PostgresDbSecrets?> GetPostgresDbSecrets();
    Task<RedisSecrets?> GetRedisSecrets();
}
