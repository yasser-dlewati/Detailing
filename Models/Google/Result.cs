using System.Text.Json.Serialization;

namespace Detailing.Models.Google;

public class Result
{
    [JsonPropertyName("geometry")]
    public Geometry Geometry { get; set; }
}