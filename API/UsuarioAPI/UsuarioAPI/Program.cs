using AspNetCore.Scalar;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Reflection;
using UsuarioAPI.Application.Mappings;
using UsuarioAPI.Application.UseCases.PacienteUseCases;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Domain.Services;
using UsuarioAPI.Infrastructure.AppDbContext;
using UsuarioAPI.Infrastructure.Repositories;
using UsuarioAPI.Infrastructure.Security;
using UsuarioAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Gerenciamento de Médicos e Pacientes",
        Version = "v1",
        Description = "A API de Gerenciamento de Médicos e Pacientes é responsável por realizar o cadastro de médicos e pacientes," +
        "além de realizar a autenticação via JWT.",
        
        Contact = new OpenApiContact
        {
            Name = "Programadores: Antonio Kauã e Mauro Roberto.",
            Email = "kaubatista545@hotmail.com"
        },

        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

string connectionString = builder.Configuration.GetConnectionString("ConnectionString") ?? string.Empty;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ConnectionString"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
    ));


builder.Services.AddAutoMapper(typeof(MappingProfile));

// Repositórios
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<ISecurityService, PasswordService>();


// Use Cases
builder.Services.AddScoped<CadastrarPacienteUseCase>();



var app = builder.Build();

// Aplicando o Middleware de exceção
app.UseMiddleware<ExceptionMiddleware>();

// Aplicar as migrations após subir a aplicação.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.MapScalarApiReference();
app.UseScalar(options =>
{
    options.UseTheme(Theme.Default);
    options.RoutePrefix = "api-docs";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
