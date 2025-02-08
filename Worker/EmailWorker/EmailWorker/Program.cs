using Microsoft.EntityFrameworkCore;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Infrastructure.AppDbContext;
using UsuarioAPI.Infrastructure.Repositories;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        // Registra a fila de e-mail como Singleton (se apropriado para seu cen�rio)
        services.AddSingleton<IEmailQueue, EmailQueue>();

        string connectionString = configuration.GetConnectionString("ConnectionString") ?? string.Empty;

        // Registra o ApplicationDbContext (ajuste a string de conex�o conforme necess�rio)
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
        connectionString,
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
    ));
        // Registra a implementa��o do reposit�rio de m�dicos
        services.AddScoped<IMedicoRepository, MedicoRepository>();

        // Registra o Worker como Hosted Service
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
