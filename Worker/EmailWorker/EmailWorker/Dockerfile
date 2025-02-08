#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Worker/EmailWorker/EmailWorker/AgendaAPI.Worker.csproj", "Worker/EmailWorker/EmailWorker/"]
COPY ["API/AgendaAPI/AgendaAPI.Infrastructure/AgendaAPI.Infrastructure.csproj", "API/AgendaAPI/AgendaAPI.Infrastructure/"]
COPY ["API/AgendaAPI/AgendaAPI.Application/AgendaAPI.Application.csproj", "API/AgendaAPI/AgendaAPI.Application/"]
COPY ["API/AgendaAPI/AgendaAPI.Domain/AgendaAPI.Domain.csproj", "API/AgendaAPI/AgendaAPI.Domain/"]
COPY ["API/UsuarioAPI/UsuarioAPI.Domain/UsuarioAPI.Domain.csproj", "API/UsuarioAPI/UsuarioAPI.Domain/"]
COPY ["API/UsuarioAPI/UsuarioAPI.Infrastructure/UsuarioAPI.Infrastructure.csproj", "API/UsuarioAPI/UsuarioAPI.Infrastructure/"]
COPY ["API/UsuarioAPI/UsuarioAPI.Application/UsuarioAPI.Application.csproj", "API/UsuarioAPI/UsuarioAPI.Application/"]
RUN dotnet restore "./Worker/EmailWorker/EmailWorker/AgendaAPI.Worker.csproj"
COPY . .
WORKDIR "/src/Worker/EmailWorker/EmailWorker"
RUN dotnet build "./AgendaAPI.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AgendaAPI.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgendaAPI.Worker.dll"]