using backend.Models;

namespace NeoVisualizer.Api.Services;

public interface INeoService
{
    Task<NeoFeedResponse?> GetRawFeedAsync(
        DateTime startDate,
        DateTime endDate,
        CancellationToken ct = default
    );
}
