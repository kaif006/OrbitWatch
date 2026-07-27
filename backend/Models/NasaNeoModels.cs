using System.Text.Json.Serialization;

namespace backend.Models;

public record NeoFeedResponse(
    [property: JsonPropertyName("element_count")] int ElementCount,
    [property: JsonPropertyName("near_earth_objects")]
        Dictionary<string, List<NearEarthObjectDto>> NearEarthObjects
);

public record NearEarthObjectDto(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("absolute_magnitude_h")] double AbsoluteMagnitudeH,
    [property: JsonPropertyName("estimated_diameter")] EstimatedDiameterDto EstimatedDiameter,
    [property: JsonPropertyName("is_potentially_hazardous_asteroid")] bool IsPotentiallyHazardous,
    [property: JsonPropertyName("close_approach_data")] List<CloseApproachDataDto> CloseApproachData
);

public record EstimatedDiameterDto(
    [property: JsonPropertyName("meters")] DistanceRangeDto Meters,
    [property: JsonPropertyName("kilometers")] DistanceRangeDto Kilometers
);

public record DistanceRangeDto(
    [property: JsonPropertyName("estimated_diameter_min")] double Min,
    [property: JsonPropertyName("estimated_diameter_max")] double Max
);

public record CloseApproachDataDto(
    [property: JsonPropertyName("close_approach_date")] string CloseApproachDate,
    [property: JsonPropertyName("relative_velocity")] VelocityDto RelativeVelocity,
    [property: JsonPropertyName("miss_distance")] MissDistanceDto MissDistance,
    [property: JsonPropertyName("orbiting_body")] string OrbitingBody
);

public record VelocityDto(
    [property: JsonPropertyName("kilometers_per_second")] string KilometersPerSecond,
    [property: JsonPropertyName("kilometers_per_hour")] string KilometersPerHour
);

public record MissDistanceDto(
    [property: JsonPropertyName("astronomical")] string Astronomical,
    [property: JsonPropertyName("lunar")] string Lunar,
    [property: JsonPropertyName("kilometers")] string Kilometers
);
