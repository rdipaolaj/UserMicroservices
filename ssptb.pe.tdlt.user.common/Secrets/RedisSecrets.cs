using ssptb.pe.tdlt.user.common.Settings;
using System.Text.Json.Serialization;

namespace ssptb.pe.tdlt.user.common.Secrets;
public class RedisSecrets : ISecret
{
    [JsonPropertyName("private-key")]
    public string PrivateKey { get; set; } = string.Empty;
}
