using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", buider =>
    {
        buider.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



builder.Services.AddSwaggerForOcelot(builder.Configuration, x =>
        {
            x.GenerateDocsForGatewayItSelf = false;
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();


app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", context =>
    {
        context.Response.Redirect("swagger/index.html");
        return Task.CompletedTask;
    });
});


app.UseSwaggerForOcelotUI(
    opt => { opt.PathToSwaggerGenerator = "/swagger/docs"; });

await app.UseOcelot();

app.Run();
