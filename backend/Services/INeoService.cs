using backend.Models;

namespace backend.Services;

public interface INeoService
{
    Task<NeoFeedResponse?> GetRawFeedAsync(
        DateTime startDate,
        DateTime endDate,
        CancellationToken ct = default
    );
}
