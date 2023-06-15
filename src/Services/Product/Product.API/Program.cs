using Contracts.Common.Interfaces;
using Contracts.Messages;
using EventBus.IntegrationEvents;
using Infrastructure.Common;
using Infrastructure.Messages;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Product.Application.Features.ProductItems.Commands.UpdateProductItem;
using Product.Domain.AggregateModels.ProductAggregate;
using Product.Infrastructure.Persistence;
using Product.Infrastructure.Repositories;
using Shared.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateProductItemCommandHandler).Assembly));
builder.Services.AddScoped<IMessageProducer, RabbitMQProducer>();
builder.Services.AddScoped<ISerializeService, SerializeService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5443";
        options.Audience = "weatherapi";
        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
    });


builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductItemRepository, ProductItemRepository>();


// Extract settings
var settings = builder.Configuration.GetSection("EventBusSettings").Get<EventBusSettings>();
if (settings == null || string.IsNullOrEmpty(settings.HostAddress))
    throw new ArgumentNullException("EventBusSettings is not configured!");

var mqConnection = new Uri(settings.HostAddress);


// Configure MassTransit
builder.Services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(mqConnection);
    });
    // Publish submit order message, instead of sending it to a specific queue directly.
    config.AddRequestClient<ProductEvent>();
});




var app = builder.Build();

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
