using Microsoft.EntityFrameworkCore;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Infrastructure.AppDbContext;
using UsuarioAPI.Infrastructure.Repositories;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        // Registra a fila de e-mail como Singleton (se apropriado para seu cenário)
        services.AddSingleton<IEmailQueue, EmailQueue>();

        string connectionString = configuration.GetConnectionString("ConnectionString") ?? string.Empty;

        // Registra o ApplicationDbContext (ajuste a string de conexão conforme necessário)
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
        connectionString,
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
    ));
        // Registra a implementação do repositório de médicos
        services.AddScoped<IMedicoRepository, MedicoRepository>();

        // Registra o Worker como Hosted Service
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
