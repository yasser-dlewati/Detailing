using System.Text.Json.Serialization;

namespace Detailing.Models.Google;

public class Geometry
{
    [JsonPropertyName("location")]
    public Location Location { get; set; }
}
