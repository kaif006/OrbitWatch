using backend.Services;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Redis Distributed Cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "NeoVis_";
});

// 2. Add Resilient Typed HttpClient for NASA
builder
    .Services.AddHttpClient<INeoService, NeoService>(client =>
    {
        string baseUrl =
            builder.Configuration["NasaApi:BaseUrl"] ?? "https://api.nasa.gov/neo/rest/v1/";
        client.BaseAddress = new Uri(baseUrl);
    })
    .AddPolicyHandler(
        HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
    );

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Map the controllers
app.MapControllers();

app.Run();
