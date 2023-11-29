using System.Text.Json.Serialization;

namespace Detailing.Models.Google;

public class ResultsContainer
{
    [JsonPropertyName("results")]
    public List<Result> Results { get; set; }
    
    [JsonPropertyName("status")]
    public string Status { get; set; }
}