using BookService.Domain.Interfaces;
using BookService.Infrastructure.Persistence.Repositories;
using BookService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using BookService.Application.Commands;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using FluentValidation;
using BookService.Application.Validators;
using BookService.Application.Common;
using BookService.Infrastructure.EventBus;
using MassTransit;
using BookService.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

ConfigureDatabase(builder);
ConfigureMediatR(builder);
ConfigureFluentValidation(builder);
ConfigureRepositories(builder);
ConfigureMassTransit(builder);
ConfigureControllers(builder);
ConfigureSwagger(builder);

var app = builder.Build();

ConfigureMiddleware(app);

app.Run();

void ConfigureDatabase(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<BookMicroserviceContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
}

void ConfigureMediatR(WebApplicationBuilder builder)
{
    builder.Services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(typeof(CreateAuthorCommand).Assembly));
}

void ConfigureFluentValidation(WebApplicationBuilder builder)
{
    builder.Services.AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();
    builder.Services.AddValidatorsFromAssemblyContaining<CreateAuthorCommandValidator>();
    builder.Services.AddScoped<ValidatorService>();
}

void ConfigureRepositories(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
    builder.Services.AddScoped<IGenreRepository, GenreRepository>();
    builder.Services.AddScoped<IBookRepository, BookRepository>();
    builder.Services.AddHttpClient();
}

void ConfigureMassTransit(WebApplicationBuilder builder)
{
    builder.Services.AddMassTransit(config =>
    {
        config.AddConsumer<BookRentedEventConsumer>();
        config.AddConsumer<BookReturnedEventConsumer>();
        config.UsingRabbitMq((ctx, cfg) =>
        {
            cfg.Host("localhost", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });

            cfg.ReceiveEndpoint("book-rented", e =>
            {
                e.ConfigureConsumer<BookRentedEventConsumer>(ctx);
            });

            cfg.ReceiveEndpoint("book-returned", e =>
            {
                e.ConfigureConsumer<BookReturnedEventConsumer>(ctx);
            });
        });
    });
    builder.Services.AddScoped<BookRentedEventConsumer>();
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

void ConfigureMiddleware(WebApplication app)
{
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}