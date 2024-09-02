using Mapster;
using ssptb.pe.tdlt.user.command.Command;
using ssptb.pe.tdlt.user.entities.Entities;

namespace sptb.pe.tdlt.user.command.Mapster;
public static class MapsterConfiguration
{
    public static TypeAdapterConfig Configuration()
    {
        TypeAdapterConfig config = new();

        // Configuración de CreateUserCommand a User
        config.NewConfig<CreateUserCommand, User>()
            .Map(dest => dest.UserId, src => Guid.NewGuid())
            .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
            .Map(dest => dest.LastLogin, src => DateTime.UtcNow)
            .Map(dest => dest.AccountStatus, src => "A");

        // Configuración de UpdateUserCommand a User
        config.NewConfig<UpdateUserCommand, User>()
            .IgnoreNullValues(true); // Ignora valores nulos en el mapeo, útil si no todos los campos se actualizan


        return config;
    }
}
