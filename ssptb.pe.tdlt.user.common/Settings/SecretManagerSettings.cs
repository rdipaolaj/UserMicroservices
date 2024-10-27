namespace ssptb.pe.tdlt.user.common.Settings;
public class SecretManagerSettings
{
    public bool Local { get; set; }
    public string Region { get; set; } = string.Empty;
    public string ArnPostgresSecrets { get; set; } = string.Empty;
    public string ArnRedisSecrets { get; set; } = string.Empty;
}
