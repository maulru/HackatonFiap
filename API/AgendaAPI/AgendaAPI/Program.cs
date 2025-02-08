using AgendaAPI.Application.Mappings;
using AgendaAPI.Application.Services;
using AgendaAPI.Application.UseCases.AgendaUseCases;
using AgendaAPI.Application.UseCases.HorarioUseCases;
using AgendaAPI.Domain.Repositories;
using AgendaAPI.Infrastructure.AppDbContext;
using AgendaAPI.Infrastructure.Repositories;
using AspNetCore.Scalar;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Reflection;
using System.Text;

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
        Title = "API de Gerenciamento de Agenda e Consultas",
        Version = "v1",
        Description = "A API de Gerenciamento de Agenda e Consultas é responsável por realizar o cadastro da agenda dos médicos," +
        "agendamento de consultas pelo paciente e aprovação/cancelamento pelos médicos.",

        Contact = new OpenApiContact
        {
            Name = "Programadores: Antonio Kauã e Mauro Roberto.",
            Email = "maurojr_@outlook.com"
        },

        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    c.SchemaFilter<SwaggerExcludeFilter>();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Digite o token JWT.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    if (File.Exists(xmlPath))
    {
        var xmlBytes = File.ReadAllBytes(xmlPath);
        var xmlContent = Encoding.UTF8.GetString(xmlBytes);
        File.WriteAllText(xmlPath, xmlContent, Encoding.UTF8);
        c.IncludeXmlComments(xmlPath);
    }

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
builder.Services.AddScoped<IAgendaRepository, AgendaRepository>();
builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();

// Use Cases
builder.Services.AddScoped<CadastrarHorarioUseCase>();
builder.Services.AddScoped<ListarAgendaMedicoUseCase>();
builder.Services.AddScoped<AlterarHorarioUseCase>();
builder.Services.AddScoped<ObterHorariosPendentesOuAgendadosUseCase>();
builder.Services.AddScoped<AlterarStatusAgendamentoUseCase>();
builder.Services.AddScoped<CadastrarAgendamentoUseCase>();
builder.Services.AddScoped<CancelarAgendamentoUseCase>();
builder.Services.AddScoped<ObterHorariosDisponiveisUseCase>();
builder.Services.AddScoped<ListarMedicoUseCase>();
builder.Services.AddScoped<ListarHorariosMedicoUseCase>();

// Services
builder.Services.AddScoped<AgendaServices>();
builder.Services.AddScoped <ConsultaServices>();

// Token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"] ?? throw new Exception("É necessário configurar a variavel SymmetricSecurityKey"))),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        dbContext.Database.Migrate();
    }
    catch
    {
        Console.WriteLine("Falha ao rodar migrations, mas continuando a execução.");
    }

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
}

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
