# Usa la imagen oficial de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia el archivo de solución y los archivos .csproj y restaura dependencias
COPY ssptb.pe.tdlt.user.sln ./
COPY ssptb.pe.tdlt.user.api/ssptb.pe.tdlt.user.api.csproj ./ssptb.pe.tdlt.user.api/
COPY ssptb.pe.tdlt.user.command/ssptb.pe.tdlt.user.command.csproj ./ssptb.pe.tdlt.user.command/
COPY ssptb.pe.tdlt.user.commandhandler/ssptb.pe.tdlt.user.commandhandler.csproj ./ssptb.pe.tdlt.user.commandhandler/
COPY ssptb.pe.tdlt.user.commandvalidator/ssptb.pe.tdlt.user.commandvalidator.csproj ./ssptb.pe.tdlt.user.commandvalidator/
COPY ssptb.pe.tdlt.user.common/ssptb.pe.tdlt.user.common.csproj ./ssptb.pe.tdlt.user.common/
COPY ssptb.pe.tdlt.user.data/ssptb.pe.tdlt.user.data.csproj ./ssptb.pe.tdlt.user.data/
COPY ssptb.pe.tdlt.user.dto/ssptb.pe.tdlt.user.dto.csproj ./ssptb.pe.tdlt.user.dto/
COPY ssptb.pe.tdlt.user.entities/ssptb.pe.tdlt.user.entities.csproj ./ssptb.pe.tdlt.user.entities/
COPY ssptb.pe.tdlt.user.infraestructure/ssptb.pe.tdlt.user.infraestructure.csproj ./ssptb.pe.tdlt.user.infraestructure/
COPY ssptb.pe.tdlt.user.internalservices/ssptb.pe.tdlt.user.internalservices.csproj ./ssptb.pe.tdlt.user.internalservices/
COPY ssptb.pe.tdlt.user.redis/ssptb.pe.tdlt.user.redis.csproj ./ssptb.pe.tdlt.user.redis/
COPY ssptb.pe.tdlt.user.secretsmanager/ssptb.pe.tdlt.user.secretsmanager.csproj ./ssptb.pe.tdlt.user.secretsmanager/

# Restaura dependencias
RUN dotnet restore

# Copia todo el código fuente y compílalo
COPY . ./
RUN dotnet publish ssptb.pe.tdlt.user.api/ -c Release -o /app/out

# Usa una imagen de runtime más ligera para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia los archivos publicados
COPY --from=build-env /app/out .

# Copia el archivo de configuración para producción
COPY ssptb.pe.tdlt.user.api/appsettings.Production.json ./appsettings.Production.json

# Configura el entorno de producción
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

# Expone el puerto que la aplicación utilizará
EXPOSE 80

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "ssptb.pe.tdlt.user.api.dll"]
