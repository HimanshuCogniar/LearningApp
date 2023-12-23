using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using LearningAPI.DataAccess;
using LearningAPI.Repositories;
using Microsoft.Extensions.Configuration;
using LearningAPI.JWT;
using Microsoft.AspNetCore.Server.Kestrel.Core;
//using IdempotentAPI;
//using IdempotentAPI.Extensions.DependencyInjection;
//using IdempotentAPI.Cache.DistributedCache.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddSingleton<IJwtAuthenticationManager>(x => new JwtAuthenticationManager(x.GetService<IConfiguration>(), builder.Configuration.GetSection("JWT:Key").Value, x.GetService<IRefreshTokenGenerator>()));
builder.Services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

//builder.Services.AddSingleton<IGenerateResponse, GenerateResponse>();
//builder.Services.AddSingleton<IEmailProvider, EmailProvider>();
builder.Services.AddSingleton<IDataAccess, DataAccess>();
//builder.Services.AddSingleton<TimerManager>();
//builder.Services.AddSingleton<DashboardService>();
//builder.Services.AddScoped<IRedisCacheHandler, RedisCacheHandler>();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1, 1);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
    x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "LearningAPI",
        Description = "Your API Description",
        Contact = new OpenApiContact
        {
            Name = "Your Name",
            Email = "your.email@example.com"
        }
    });

    // Include the comments from the controller
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LearningAPI v1");
        // Add any additional Swagger configurations as needed
    });
}

//app.UseHttpsRedirection();

// UseRouting should come before UseEndpoints
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    // endpoints.MapHub<DashboardHub>("/dashboardhub");
    // endpoints.MapHub<ChatHub>("/chathub");
});

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
