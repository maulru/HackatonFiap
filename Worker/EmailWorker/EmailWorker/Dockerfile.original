# Imagem base com ASP.NET Core (contém o Microsoft.AspNetCore.App)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Fase de build usando o SDK do .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Ajuste o caminho do projeto conforme sua estrutura. Aqui, supondo que o projeto se chame AgendaAPI.Worker
COPY ["AgendaAPI.Worker/AgendaAPI.Worker.csproj", "AgendaAPI.Worker/"]
RUN dotnet restore "./AgendaAPI.Worker/AgendaAPI.Worker.csproj"
COPY . .
WORKDIR "/src/AgendaAPI.Worker"
RUN dotnet build "./AgendaAPI.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase de publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AgendaAPI.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final baseada no ASP.NET Core runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgendaAPI.Worker.dll"]
