using Basket.Application.IntegrationEvents.EventHandling;
using Basket.Application.IntegrationEvents.Events;
using Basket.Application.MessageSubcribers;
using Basket.Domain.AggregateModels.BasketAggregate;
using Basket.Infrastructure.Persistence;
using Basket.Infrastructure.Repositories;
using Contracts.Common.Interfaces;
using EventBus.Abstractions;
using EventBus.IntegrationEvents;
using Infrastructure.Common;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Configurations;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<BasketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISerializeService, SerializeService>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5443";
        options.Audience = "weatherapi";
        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
    });

/*builder.Services.AddGrpcClient<Greeter.GreeterClient>(options =>
{
    options.Address = new Uri("http://localhost:5174"); // Replace with the actual gRPC server URL
});*/



builder.Services.AddTransient<IIntegrationEventHandler<ProductPriceChangedIntegrationEvent>, ProductPriceChangedIntegrationEventHandler>();


// Extract settings
var settings = builder.Configuration.GetSection("EventBusSettings").Get<EventBusSettings>();
if (settings == null || string.IsNullOrEmpty(settings.HostAddress))
    throw new ArgumentNullException("EventBusSettings is not configured!");

var mqConnection = new Uri(settings.HostAddress);


// Configure MassTransit
builder.Services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
builder.Services.AddMassTransit(config =>
{
    config.AddConsumersFromNamespaceContaining<ProductPriceChangedIntegrationEventMassTransitHandler>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(mqConnection);
        /* cfg.ReceiveEndpoint("product-price-changes2", c =>
         {
             c.ConfigureConsumer<ProductPriceChangedIntegrationEventMassTransitHandler>(ctx);
         });*/
        cfg.ConfigureEndpoints(ctx);
    });
    //submit fanout
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//subcribe to other serivces
// Instantiate the class

// Call the method to subscribe to orders
var productUpdate = new ProductPriceChangedIntegrationEventSubcribe();
productUpdate.Subscribe();

var productDelete = new ProductDeleteIntegrationEventSubscribe();
productDelete.Subscribe();

app.Run();


