# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5002

# Definir ambiente UTF-8 corretamente
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LANG=pt_BR.UTF-8
ENV LC_ALL=pt_BR.UTF-8

# Instalar suporte a internacionalização caso a imagem seja baseada em Alpine (opcional)
RUN apt-get update && apt-get install -y locales && \
    locale-gen pt_BR.UTF-8 && \
    dpkg-reconfigure --frontend=noninteractive locales

# Alterando dockerfile para expor a porta 5002
ENV ASPNETCORE_URLS=http://+:5002

# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["API/AgendaAPI/AgendaAPI/AgendaAPI.csproj", "API/AgendaAPI/AgendaAPI/"]
COPY ["API/AgendaAPI/AgendaAPI.Application/AgendaAPI.Application.csproj", "API/AgendaAPI/AgendaAPI.Application/"]
COPY ["API/AgendaAPI/AgendaAPI.Domain/AgendaAPI.Domain.csproj", "API/AgendaAPI/AgendaAPI.Domain/"]
COPY ["API/AgendaAPI/AgendaAPI.Infrastructure/AgendaAPI.Infrastructure.csproj", "API/AgendaAPI/AgendaAPI.Infrastructure/"]
RUN dotnet restore "./API/AgendaAPI/AgendaAPI/AgendaAPI.csproj"
COPY . .
WORKDIR "/src/API/AgendaAPI/AgendaAPI"
RUN dotnet build "./AgendaAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AgendaAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgendaAPI.dll"]