using RentService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using RentService.Application.Commands;
using System.Text.Json.Serialization;
using RentService.Infrastructure.Persistence.Repositories;
using RentService.Infrastructure.Persistence;
using MassTransit;
using RentService.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigureDatabase(builder);
ConfigureMediatR(builder);
ConfigureRepositories(builder);
ConfigureMassTransit(builder);
ConfigureControllers(builder);
ConfigureSwagger(builder);
ConfigureServices(builder);

var app = builder.Build();

ConfigureMiddleware(app);

app.Run();

void ConfigureDatabase(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<RentMicroserviceContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
}

void ConfigureMediatR(WebApplicationBuilder builder)
{
    builder.Services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(typeof(RegisterLibrarianCommand).Assembly));
}

void ConfigureRepositories(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<ILibrarianRepository, LibrarianRepository>();
    builder.Services.AddScoped<IRentalRepository, RentalRepository>();
    builder.Services.AddScoped<IRenterRepository, RenterRepository>();
    builder.Services.AddScoped<IStatusRepository, StatusRepository>();

    builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

    // Регистрируем JwtSettings как сервис
    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
    builder.Services.AddSingleton(resolver =>
        resolver.GetRequiredService<IOptions<JwtSettings>>().Value);

    builder.Services.AddScoped<ITokenProvider, JwtProvider>();

    builder.Services.AddHttpClient();
}

void ConfigureMassTransit(WebApplicationBuilder builder)
{
    builder.Services.AddMassTransit(config =>
    {
        config.UsingRabbitMq((ctx, cfg) =>
        {
            cfg.Host("localhost", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
        });
    });
}

void ConfigureControllers(WebApplicationBuilder builder)
{
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
}

void ConfigureSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

void ConfigureServices(WebApplicationBuilder builder)
{
    
    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["cookies_"];
                return Task.CompletedTask;
            }
        };
    });
    builder.Services.AddHttpContextAccessor();
}

void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication(); // Добавьте эту строку
    app.UseAuthorization();
    app.MapControllers();
}