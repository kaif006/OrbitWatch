using System.Text.Json;
using backend.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace backend.Services;

public class NeoService : INeoService
{
    private readonly HttpClient _httpClient;
    private readonly IDistributedCache _cache;
    private readonly IConfiguration _config;
    private readonly ILogger<NeoService> _logger;

    public NeoService(
        HttpClient httpClient,
        IDistributedCache cache,
        IConfiguration config,
        ILogger<NeoService> logger
    )
    {
        _httpClient = httpClient;
        _cache = cache;
        _config = config;
        _logger = logger;
    }

    public async Task<NeoFeedResponse> GetRawFeedAsync(
        DateTime startDate,
        DateTime endDate,
        CancellationToken ct = default
    )
    {
        string startStr = startDate.ToString("yyyy-MM-dd");
        string endStr = endDate.ToString("yyyy-MM-dd");
        string cacheKey = $"neo:rawfeed:{startStr}:{endStr}";

        // Try reading from Redis Cache
        var cachedJson = await _cache.GetStringAsync(cacheKey, ct);
        if (!String.IsNullOrEmpty(cachedJson))
        {
            _logger.LogInformation("Cache Hit for key: {CacheKey}", cacheKey);
            return JsonSerializer.Deserialize<NeoFeedResponse>(cachedJson);
        }

        _logger.LogInformation("Cache Miss for key: {CacheKey}. Querying NASA API...", cacheKey);

        // Fetch from NASA API
        string apiKey = _config["NasaApi:ApiKey"] ?? "DemoKey";
        string endpoint = $"feed?start_date={startStr}&end_date={endStr}&api_key={apiKey}";

        var response = await _httpClient.GetAsync(endpoint, ct);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(ct);
        var feed = JsonSerializer.Deserialize<NeoFeedResponse>(json);

        // Cache in Redis for 12 hourse if succesfull
        if (feed != null)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12),
            };
            await _cache.SetStringAsync(cacheKey, json, cacheOptions, ct);
        }

        return feed;
    }
}
