using ssptb.pe.tdlt.user.common.Settings;
using System.Text.Json.Serialization;

namespace ssptb.pe.tdlt.user.common.Secrets;
public class PostgresDbSecrets : ISecret
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("engine")]
    public string Engine { get; set; } = string.Empty;

    [JsonPropertyName("host")]
    public string Host { get; set; } = string.Empty;

    [JsonPropertyName("port")]
    public int Port { get; set; }

    [JsonPropertyName("dbname")]
    public string Dbname { get; set; } = string.Empty;

    [JsonPropertyName("dbInstanceIdentifier")]
    public string DbInstanceIdentifier { get; set; } = string.Empty;
}
