using RentService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using RentService.Application.Commands;
using System.Text.Json.Serialization;
using RentService.Infrastructure.Persistence.Repositories;
using RentService.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RentMicroserviceContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateLibrarianCommand).Assembly));

builder.Services.AddScoped<ILibrarianRepository, LibrarianRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IRenterRepository, RenterRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddHttpClient();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
