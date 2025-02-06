using AspNetCore.Scalar;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using UsuarioAPI.Application.Mappings;
using UsuarioAPI.Application.Services;
using UsuarioAPI.Application.UseCases.MedicoUseCases;
using UsuarioAPI.Application.UseCases.PacienteUseCases;
using UsuarioAPI.Domain.Entities.Base;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Domain.Services;
using UsuarioAPI.Infrastructure.AppDbContext;
using UsuarioAPI.Infrastructure.Repositories;
using UsuarioAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


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
    c.UseInlineDefinitionsForEnums();
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
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioValidatorService, UsuarioValidatorService>();

// Use Cases
builder.Services.AddScoped<CadastrarUsuarioUseCase>();
builder.Services.AddScoped<CadastrarMedicoUseCase>();
builder.Services.AddScoped<ObterMedicosDisponiveisUseCase>();
builder.Services.AddScoped<CadastrarPacienteUseCase>();

// Services
builder.Services
    .AddIdentity<UsuarioBase, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders(); //Geração de Tokens

builder.Services.AddScoped<UsuarioServices>();
builder.Services.AddScoped<TokenService>();

// Token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero

    };
}
);

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
