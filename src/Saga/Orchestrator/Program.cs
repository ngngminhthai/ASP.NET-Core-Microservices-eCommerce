using Orchestrator.HttpRepositories.Interfaces;
using Orchestrator.HttpRepository;
using Orchestrator.HttpRepository.Interfaces;
using Orchestrator.Services;
using Orchestrator.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IOrderHttpRepository, OrderHttpRepository>();
builder.Services.AddScoped<IBasketHttpRepository, BasketHttpRepository>();
builder.Services.AddScoped<IInventoryHttpRepository, InventoryHttpRepository>();
builder.Services.AddScoped<ICheckoutSagaService, CheckoutSagaService>();

builder.Services.AddHttpClient<IOrderHttpRepository, OrderHttpRepository>("OrdersAPI", (s, c) =>
{
    c.BaseAddress = new Uri("http://localhost:5005/api/");
});

builder.Services.AddHttpClient<IBasketHttpRepository, BasketHttpRepository>("BasketsAPI", (s, c) =>
{
    c.BaseAddress = new Uri("http://localhost:5003/api/");
});

builder.Services.AddHttpClient<IInventoryHttpRepository, InventoryHttpRepository>("InventoryAPI", (s, c) =>
{
    c.BaseAddress = new Uri("http://localhost:5005/api/");
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
