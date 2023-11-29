using System.Text.Json.Serialization;

namespace Detailing.Models.Google;

public class Location
{
    [JsonPropertyName("lat")]
    public double Latitude { get; set; }

    [JsonPropertyName("lng")]
    public double Longitude { get; set; }
}