using Basket.Application.MessageSubcribers;
using Contracts.Common.Interfaces;
using Contracts.Common.Messages;
using Infrastructure.Common;
using Infrastructure.Messages;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMessageConsumer, RabbitMQConsumer>();
builder.Services.AddScoped<ISerializeService, SerializeService>();

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
var subscription = new ProductPriceChangedIntegrationEventSubcribe();

// Call the method to subscribe to orders
var subscriber = new ProductPriceChangedIntegrationEventSubcribe();
subscriber.Subscribe();

app.Run();


