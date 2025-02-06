using AgendaAPI.Application.Mappings;
using AgendaAPI.Application.Services;
using AgendaAPI.Application.UseCases.AgendaUseCases;
using AgendaAPI.Application.UseCases.HorarioUseCases;
using AgendaAPI.Domain.Repositories;
using AgendaAPI.Infrastructure.AppDbContext;
using AgendaAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
  //  c.IncludeXmlComments(xmlPath);
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

// Aplicar as migrations após subir a aplicação.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

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
