# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Base para execução da aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 8080  

# Definir ambiente UTF-8 corretamente
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LANG=pt_BR.UTF-8
ENV LC_ALL=pt_BR.UTF-8

# Instalar suporte a internacionalização caso a imagem seja baseada em Alpine (opcional)
RUN apt-get update && apt-get install -y locales && \
    locale-gen pt_BR.UTF-8 && \
    dpkg-reconfigure --frontend=noninteractive locales

# Forçar a aplicação a rodar na porta 8080
ENV ASPNETCORE_URLS=http://+:8080


# Fase de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar os arquivos do projeto
COPY ["API/UsuarioAPI/UsuarioAPI/UsuarioAPI.csproj", "API/UsuarioAPI/UsuarioAPI/"]
COPY ["API/UsuarioAPI/UsuarioAPI.Application/UsuarioAPI.Application.csproj", "API/UsuarioAPI/UsuarioAPI.Application/"]
COPY ["API/UsuarioAPI/UsuarioAPI.Domain/UsuarioAPI.Domain.csproj", "API/UsuarioAPI/UsuarioAPI.Domain/"]
COPY ["API/UsuarioAPI/UsuarioAPI.Infrastructure/UsuarioAPI.Infrastructure.csproj", "API/UsuarioAPI/UsuarioAPI.Infrastructure/"]

# Restaurar pacotes
RUN dotnet restore "./API/UsuarioAPI/UsuarioAPI/UsuarioAPI.csproj"

# Copiar todo o código-fonte
COPY . .

# Compilar a aplicação
WORKDIR "/src/API/UsuarioAPI/UsuarioAPI"
RUN dotnet build "./UsuarioAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UsuarioAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final - produção
FROM base AS final
WORKDIR /app

# Copiar os arquivos publicados para a imagem final
COPY --from=publish /app/publish .

# Definir o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "UsuarioAPI.dll"]
